using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class FilaAtendimentoService : BaseService<FilaAtendimento>, IFilaAtendimentoService
    {

        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;
        private readonly IPessoaPacienteService _servicePaciente;
        private readonly IFilaAtendimentoEventoService _serviceFilaAtendimentoEvento;
        private readonly IClassificacaoRiscoHistoricoService _serviceClassificacaoRiscoHistorico;

        public FilaAtendimentoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
            _serviceClassificacaoRiscoHistorico = new ClassificacaoRiscoHistoricoService(contextDominio, contextKlinikos, context);
            _serviceFilaAtendimentoEvento =  new FilaAtendimentoEventoService(contextDominio, contextKlinikos, context);
            _servicePaciente = new PessoaPacienteService(contextDominio, contextKlinikos, context);
        }

        public async Task<CustomResponse<IList<FilaAtendimento>>> ConsultarFila()
        {
            var _response = new CustomResponse<IList<FilaAtendimento>>();


            try
            {
                var lista = await _contextKlinikos.FilaAtendimento.Where(x => x.Ativo).Include(fila=>fila.ClassificacaoRisco).Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente).ToListAsync();
                _response.StatusCode = StatusCodes.Status200OK;
                _response.Result = lista;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaAtendimento>> AdicionarPacienteFila(FilaAtendimento filaAtendimento, Guid userId)
        {
            var _response = new CustomResponse<FilaAtendimento>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                await this.Adicionar(filaAtendimento, userId);

                await _serviceClassificacaoRiscoHistorico.AdicionarHistoricoClassificacaoRisco(filaAtendimento.ClassificacaoRisco, _pessoaMaster);

                var _filaAtendimentoEvento = new FilaAtendimentoEvento
                {
                    FilaAtendimento = filaAtendimento,
                    DataFilaAtendimentoEvento = filaAtendimento.DataEntradaFilaAtendimento,
                    EventoId = _contextDominio.Eventos.Where(x => x.Sigla == "A").FirstOrDefault().EventoId,
                    PessoaProfissional = filaAtendimento.ClassificacaoRisco.PessoaProfissional

                };


               await _serviceFilaAtendimentoEvento.Adicionar(_filaAtendimentoEvento, userId);


                _response.StatusCode = StatusCodes.Status201Created;
                _response.Message = "Incluído com sucesso";


            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaAtendimento>> RetirarPacienteFila(FilaAtendimento filaAtendimento, Guid userId)
        {
            var _response = new CustomResponse<FilaAtendimento>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                await this.Atualizar(filaAtendimento, userId);

                var _pessoaStatusId = _contextDominio.PessoaStatus.Where(x => x.Sigla == "FE").FirstOrDefault().PessoaStatusId;
                filaAtendimento.ClassificacaoRisco.PessoaPaciente.PessoaStatusId = _pessoaStatusId;

                await _servicePaciente.AtualizarPaciente(filaAtendimento.ClassificacaoRisco.PessoaPaciente, userId);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = filaAtendimento;
                _response.Message = "retirado com sucesso";

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }
    }
}
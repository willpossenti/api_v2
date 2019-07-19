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
    public class FilaClassificacaoService : BaseService<FilaClassificacao>, IFilaClassificacaoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;
        private readonly IPessoaPacienteService _servicePaciente;
        private readonly IPessoaHistoricoService _servicePessoaHistorico;
        private readonly IRegistroBoletimHistoricoService _serviceRegistroBoletimHistorico;
        private readonly IFilaClassificacaoEventoService _serviceFilaClassificacaoEvento;

        public FilaClassificacaoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
            _servicePessoaHistorico = new PessoaHistoricoService(contextDominio, contextKlinikos, context);
            _serviceRegistroBoletimHistorico = new RegistroBoletimHistoricoService(contextDominio, contextKlinikos, context);
            _serviceFilaClassificacaoEvento = new FilaClassificacaoEventoService(contextDominio, contextKlinikos, context);
            _servicePaciente = new PessoaPacienteService(contextDominio, contextKlinikos, context);
        }

        public async Task<CustomResponse<IList<FilaClassificacao>>> ConsultarFila()
        {
            var _response = new CustomResponse<IList<FilaClassificacao>>();


            try
            {
                var lista = await _contextKlinikos.FilaClassificacao.Where(x => x.Ativo).Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                    .Include(acolhimento=> acolhimento.Acolhimento).ToListAsync();
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

        public async Task<CustomResponse<FilaClassificacao>> BuscarFilaClassificacaoPorId(Guid filaClassificacaoId, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacao>();


            try
            {
                var filaclassificacao = await _contextKlinikos.FilaClassificacao.Where(x => x.FilaClassificacaoId == filaClassificacaoId && x.Ativo).Include(fila=>fila.Acolhimento)
                    .Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente).FirstOrDefaultAsync();
                _response.StatusCode = StatusCodes.Status200OK;
                _response.Result = filaclassificacao;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;

        }

        public async Task<CustomResponse<FilaClassificacao>> AdicionarPacienteFila(FilaClassificacao filaClassificacao, Guid userId)
        {
            var _response = new CustomResponse<FilaClassificacao>();

            try
            {

                var _pacienteJaRegistado = false;

                //Valida se já existe registro para o paciente
                if (filaClassificacao.RegistroBoletim.PessoaPaciente.PessoaId != Guid.Empty)
                    _pacienteJaRegistado = _contextKlinikos.FilaClassificacao.Any(x => x.RegistroBoletim.PessoaPaciente.PessoaId == filaClassificacao.RegistroBoletim.PessoaPaciente.PessoaId && x.Ativo);

                if (_pacienteJaRegistado)
                {
                    _response.StatusCode = StatusCodes.Status409Conflict;
                    _response.Message = "Paciente já registado";
                    _response.Result = filaClassificacao;
                    return _response;
                }


                var numeroBoletim = _contextKlinikos.RegistrosBoletim.Max(x => x.NumeroBoletim);

                if (numeroBoletim != null)
                {
                    var novoCodigo = int.Parse(numeroBoletim);
                    novoCodigo++;
                    filaClassificacao.RegistroBoletim.NumeroBoletim = novoCodigo.ToString("000000");
                }
                else
                    filaClassificacao.RegistroBoletim.NumeroBoletim = "000001";


                if (filaClassificacao.RegistroBoletim.PessoaPaciente != null)
                    if (filaClassificacao.RegistroBoletim.PessoaPaciente.PessoaId != null)
                        await _servicePaciente.AtualizarPaciente(filaClassificacao.RegistroBoletim.PessoaPaciente, userId);
                

                await this.Adicionar(filaClassificacao, userId);

                if(filaClassificacao.RegistroBoletim.PessoaPaciente == null)
                    await _servicePessoaHistorico.AdicionarHistoricoPaciente(filaClassificacao.RegistroBoletim.PessoaPaciente, filaClassificacao.RegistroBoletim.PessoaProfissional);



                await _serviceRegistroBoletimHistorico.AdicionarHistoricoRegistroBoletim(filaClassificacao.RegistroBoletim, filaClassificacao.RegistroBoletim.PessoaProfissional);

                var _filaClassificacaoEvento = new FilaClassificacaoEvento
                {
                    FilaClassificacao = filaClassificacao,
                    DataFilaClassificacaoEvento = filaClassificacao.DataEntradaFilaClassificacao,
                    EventoId = _contextDominio.Eventos.Where(x => x.Sigla == "A").FirstOrDefault().EventoId,
                    PessoaProfissional = filaClassificacao.RegistroBoletim.PessoaProfissional

                };


                await _serviceFilaClassificacaoEvento.Adicionar(_filaClassificacaoEvento, userId);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Message = "Incluído com sucesso";
                _response.Result = filaClassificacao;



            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaClassificacao>> RetirarPacienteFila(FilaClassificacao filaClassificacao, Guid userId)
        {
            var _response = new CustomResponse<FilaClassificacao>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                await this.Atualizar(filaClassificacao, userId);

                var _pessoaStatusId = _contextDominio.PessoaStatus.Where(x => x.Sigla == "FE").FirstOrDefault().PessoaStatusId;
                filaClassificacao.RegistroBoletim.PessoaPaciente.PessoaStatusId = _pessoaStatusId;

                await _servicePaciente.AtualizarPaciente(filaClassificacao.RegistroBoletim.PessoaPaciente, userId);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = filaClassificacao;
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
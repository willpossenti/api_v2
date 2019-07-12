using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class FilaRegistroService : BaseService<FilaRegistro>, IFilaRegistroService
    {
        private readonly IAcolhimentoHistoricoService _serviceAcolhimentoHistorico;
        private readonly IFilaRegistroEventoService _serviceFilaRegistroEvento;
        private readonly IPessoaPacienteService _servicePaciente;
        private readonly IPessoaHistoricoService _servicePessoaHistorico;
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;
        public FilaRegistroService(DominioDbContext dominioDbContext, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = dominioDbContext;
            _serviceAcolhimentoHistorico = new AcolhimentoHistoricoService(dominioDbContext, contextKlinikos, context);
            _servicePessoaHistorico = new PessoaHistoricoService(dominioDbContext, contextKlinikos, context);
            _serviceFilaRegistroEvento = new FilaRegistroEventoService(dominioDbContext, contextKlinikos, context);
            _servicePaciente = new PessoaPacienteService(dominioDbContext, contextKlinikos, context);
        }

        public async Task<CustomResponse<IList<FilaRegistro>>> ConsultarFila()
        {
            var _response = new CustomResponse<IList<FilaRegistro>>();


            try
            {
                var lista = await _contextKlinikos.FilaRegistro.Where(x=>x.Ativo).Include(fila=> fila.Acolhimento).ThenInclude(pessoa=>pessoa.PessoaPaciente).ToListAsync();
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


        public async Task<CustomResponse<FilaRegistro>> BuscarFilaRegistroPorId(Guid filaRegistroId, Guid userId) {

            var _response = new CustomResponse<FilaRegistro>();


            try
            {
                var filaregistro = await _contextKlinikos.FilaRegistro.Where(x => x.FilaRegistroId == filaRegistroId && x.Ativo).Include(fila => fila.Acolhimento).ThenInclude(pessoa=>pessoa.PessoaPaciente).FirstOrDefaultAsync();
                _response.StatusCode = StatusCodes.Status200OK;
                _response.Result = filaregistro;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;

        }


        public async Task<CustomResponse<FilaRegistro>> AdicionarPacienteFila(FilaRegistro filaRegistro, Guid userId)
        {
            var _response = new CustomResponse<FilaRegistro>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                var _pacienteJaAcolhido = false;

                if (filaRegistro.Acolhimento.PessoaPaciente.PessoaId != Guid.Empty)
                    _pacienteJaAcolhido = _contextKlinikos.FilaRegistro.Any(x=>x.Acolhimento.PessoaPaciente.PessoaId == filaRegistro.Acolhimento.PessoaPaciente.PessoaId && x.Ativo);

                if (!_pacienteJaAcolhido)
                    await this.Adicionar(filaRegistro, userId);
                else {

                    _response.StatusCode = StatusCodes.Status409Conflict;
                    _response.Message = "Paciente já acolhido";
                    return _response;

                }


                if (filaRegistro.Acolhimento != null)
                {
                    await _serviceAcolhimentoHistorico.AdicionarHistoricoAcolhimento(filaRegistro.Acolhimento, _pessoaMaster);


                    if (filaRegistro.Acolhimento.PessoaPaciente != null)
                        await _servicePessoaHistorico.AdicionarHistoricoPaciente(filaRegistro.Acolhimento.PessoaPaciente, _pessoaMaster);


                    var _filaRegistroEvento = new FilaRegistroEvento
                    {
                        FilaRegistro = filaRegistro,
                        DataFilaRegistroEvento = filaRegistro.DataEntradaFilaRegistro,
                        EventoId = _contextDominio.Eventos.Where(x=>x.Sigla == "A").FirstOrDefault().EventoId,
                        PessoaProfissional = filaRegistro.Acolhimento.PessoaProfissional

                    };


                    await _serviceFilaRegistroEvento.Adicionar(_filaRegistroEvento, userId);
                }



                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = filaRegistro;
                _response.Message = "Incluído com sucesso";

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaRegistro>> RetirarPacienteFila(FilaRegistro filaRegistro, Guid userId)
        {
            var _response = new CustomResponse<FilaRegistro>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                await this.Atualizar(filaRegistro, userId);

                var _pessoaStatusId = _contextDominio.PessoaStatus.Where(x => x.Sigla == "F").FirstOrDefault().PessoaStatusId;
                filaRegistro.Acolhimento.PessoaPaciente.PessoaStatusId = _pessoaStatusId;

                await _servicePaciente.AtualizarPaciente(filaRegistro.Acolhimento.PessoaPaciente, userId);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = filaRegistro;
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
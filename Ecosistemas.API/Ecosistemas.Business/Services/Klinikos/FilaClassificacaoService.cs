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
        }

        public async Task<CustomResponse<IList<FilaClassificacao>>> ConsultarFila()
        {
            var _response = new CustomResponse<IList<FilaClassificacao>>();


            try
            {
                var lista = await _contextKlinikos.FilaClassificacao.Where(x => x.Ativo).Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente).Include(x=>x.Acolhimento).ToListAsync();
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

        public async Task<CustomResponse<FilaClassificacao>> AdicionarPacienteFila(FilaClassificacao filaClassificacao, Guid userId)
        {
            var _response = new CustomResponse<FilaClassificacao>();

            try
            {

                var numeroBoletim = _contextKlinikos.RegistrosBoletim.Max(x => x.NumeroBoletim);

                if (numeroBoletim != null)
                {
                    var novoCodigo = int.Parse(numeroBoletim);
                    novoCodigo++;
                    filaClassificacao.RegistroBoletim.NumeroBoletim = novoCodigo.ToString("000000");
                }
                else
                    filaClassificacao.RegistroBoletim.NumeroBoletim = "000001";


                await this.Adicionar(filaClassificacao, userId);

                if (filaClassificacao.RegistroBoletim.PessoaPaciente != null)
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


    }
}
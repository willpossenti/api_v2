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

namespace Ecosistemas.Business.Services.Klinikos
{
    public class RegistroBoletimService : BaseService<RegistroBoletim>, IRegistroBoletimService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly IPessoaHistoricoService _servicePessoaHistorico;
        private readonly IRegistroBoletimHistoricoService _serviceRegistroBoletimHistorico;

        public RegistroBoletimService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _servicePessoaHistorico = new PessoaHistoricoService(contextDominio, contextKlinikos, context);
            _serviceRegistroBoletimHistorico = new RegistroBoletimHistoricoService(contextDominio, contextKlinikos, context);
        }

        public async Task<CustomResponse<RegistroBoletim>> AdicionarRegistroBoletim(RegistroBoletim registroBoletim, Guid userId)
        {
            var _response = new CustomResponse<RegistroBoletim>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();
                var numeroBoletim = _contextKlinikos.RegistrosBoletim.Max(x => x.NumeroBoletim);

                if (numeroBoletim != null)
                {
                    var novoCodigo = int.Parse(numeroBoletim);
                    novoCodigo++;
                    registroBoletim.NumeroBoletim = novoCodigo.ToString("000000");
                }
                else
                    registroBoletim.NumeroBoletim = "000001";

                registroBoletim.Ativo = true;

                await this.Adicionar(registroBoletim, userId);

                if (registroBoletim.Pessoa != null)
                    await _servicePessoaHistorico.AdicionarHistoricoPaciente(registroBoletim.Pessoa, _pessoaMaster);

                await _serviceRegistroBoletimHistorico.AdicionarHistoricoRegistroBoletim(registroBoletim, _pessoaMaster);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Message = "Incluído com sucesso";
                _response.Result = registroBoletim;
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
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
    public class AcolhimentoService : BaseService<Acolhimento>, IAcolhimentoService
    {

        private readonly IAcolhimentoHistoricoService _serviceAcolhimentoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AcolhimentoService(DominioDbContext dominioDbContext, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAcolhimentoHistorico = new AcolhimentoHistoricoService(dominioDbContext, contextKlinikos, context);
        }

        public async Task<CustomResponse<Acolhimento>> AdicionarAcolhimento(Acolhimento acolhimento, Guid userId)
        {
            var _response = new CustomResponse<Acolhimento>();

            try
            {
               var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                acolhimento.Ativo = true;

                await this.Adicionar(acolhimento, userId);

                await _serviceAcolhimentoHistorico.AdicionarHistoricoAcolhimento(acolhimento, _pessoaMaster);

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
    }
}

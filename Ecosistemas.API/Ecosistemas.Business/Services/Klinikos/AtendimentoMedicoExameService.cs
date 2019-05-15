using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
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
    public class AtendimentoMedicoExameService : BaseService<AtendimentoMedicoExame>, IAtendimentoMedicoExameService
    {

        private IAtendimentoMedicoExameHistoricoService _serviceAtendimentoMedicoExameHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoExameService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoExameHistorico = new AtendimentoMedicoExameHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<AtendimentoMedicoExame>> AdicionarAtendimentoMedicoExame(AtendimentoMedicoExame atendimentoMedicoExame, Guid userId)
        {
            var _response = new CustomResponse<AtendimentoMedicoExame>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                atendimentoMedicoExame.Ativo = true;

                await this.Adicionar(atendimentoMedicoExame, userId);

                await _serviceAtendimentoMedicoExameHistorico.AdicionarHistoricoAtendimentoMedicoExame(atendimentoMedicoExame, _pessoaMaster);

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

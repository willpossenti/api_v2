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
    public class AtendimentoMedicoAlergiaService : BaseService<AtendimentoMedicoAlergia>, IAtendimentoMedicoAlergiaService
    {

        private IAtendimentoMedicoAlergiaHistoricoService _serviceAtendimentoMedicoAlergiaHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoAlergiaService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoAlergiaHistorico = new AtendimentoMedicoAlergiaHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<AtendimentoMedicoAlergia>> AdicionarAtendimentoMedicoAlergia(AtendimentoMedicoAlergia atendimentoMedicoAlergia, Guid userId)
        {
            var _response = new CustomResponse<AtendimentoMedicoAlergia>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                atendimentoMedicoAlergia.Ativo = true;

                await this.Adicionar(atendimentoMedicoAlergia, userId);

                await _serviceAtendimentoMedicoAlergiaHistorico.AdicionarHistoricoAtendimentoMedicoAlergia(atendimentoMedicoAlergia, _pessoaMaster);

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

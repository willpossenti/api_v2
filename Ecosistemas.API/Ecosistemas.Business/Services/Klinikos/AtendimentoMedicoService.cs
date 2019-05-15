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
    public class AtendimentoMedicoService : BaseService<AtendimentoMedico>, IAtendimentoMedicoService
    {

        private IAtendimentoMedicoHistoricoService _serviceAtendimentoMedicoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoHistorico = new AtendimentoMedicoHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<AtendimentoMedico>> AdicionarAtendimentoMedico(AtendimentoMedico atendimentoMedico, Guid userId)
        {
            var _response = new CustomResponse<AtendimentoMedico>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                atendimentoMedico.Ativo = true;

                await this.Adicionar(atendimentoMedico, userId);

                await _serviceAtendimentoMedicoHistorico.AdicionarHistoricoAtendimentoMedico(atendimentoMedico, _pessoaMaster);

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

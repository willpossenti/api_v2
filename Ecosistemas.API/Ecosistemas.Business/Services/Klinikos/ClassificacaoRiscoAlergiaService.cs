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
    public class ClassificacaoRiscoAlergiaService : BaseService<ClassificacaoRiscoAlergia>, IClassificacaoRiscoAlergiaService
    {

        private IClassificacaoRiscoAlergiaHistoricoService _serviceClassificacaoRiscoAlergiaHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public ClassificacaoRiscoAlergiaService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceClassificacaoRiscoAlergiaHistorico = new ClassificacaoRiscoAlergiaHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<ClassificacaoRiscoAlergia>> AdicionarClassificacaoRiscoAlergia(ClassificacaoRiscoAlergia classificacaoRiscoAlergia, Guid userId)
        {
            var _response = new CustomResponse<ClassificacaoRiscoAlergia>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                classificacaoRiscoAlergia.Ativo = true;

                await this.Adicionar(classificacaoRiscoAlergia, userId);

                await _serviceClassificacaoRiscoAlergiaHistorico.AdicionarHistoricoClassificacaoRiscoAlergia(classificacaoRiscoAlergia, _pessoaMaster);

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

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
    public class ClassificacaoRiscoService : BaseService<ClassificacaoRisco>, IClassificacaoRiscoService
    {

        private IClassificacaoRiscoHistoricoService _serviceClassificacaoRiscoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public ClassificacaoRiscoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceClassificacaoRiscoHistorico = new ClassificacaoRiscoHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<ClassificacaoRisco>> AdicionarClassificacaoRisco(ClassificacaoRisco classificacaoRisco, Guid userId)
        {
            var _response = new CustomResponse<ClassificacaoRisco>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                classificacaoRisco.Ativo = true;

                await this.Adicionar(classificacaoRisco, userId);

                await _serviceClassificacaoRiscoHistorico.AdicionarHistoricoClassificacaoRisco(classificacaoRisco, _pessoaMaster);

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

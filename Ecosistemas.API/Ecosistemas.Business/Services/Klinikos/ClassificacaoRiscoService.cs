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
        private readonly KlinikosDbContext _context;
        private IClassificacaoRiscoHistoricoService _serviceClassificacaoRiscoHistorico;

        public ClassificacaoRiscoService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _serviceClassificacaoRiscoHistorico = new ClassificacaoRiscoHistoricoService(context);
        }

        public async Task<CustomResponse<ClassificacaoRisco>> AdicionarClassificacaoRisco(ClassificacaoRisco classificacaoRisco, Guid userId)
        {
            var _response = new CustomResponse<ClassificacaoRisco>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();


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

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
using Microsoft.EntityFrameworkCore;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class ClassificacaoRiscoService : BaseService<ClassificacaoRisco>, IClassificacaoRiscoService
    {

        private readonly IClassificacaoRiscoHistoricoService _serviceClassificacaoRiscoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public ClassificacaoRiscoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceClassificacaoRiscoHistorico = new ClassificacaoRiscoHistoricoService(contextDominio, contextKlinikos, context);
        }

        public async Task<CustomResponse<ClassificacaoRisco>> AdicionarClassificacaoRisco(ClassificacaoRisco classificacaoRisco, Guid userId)
        {
            var _response = new CustomResponse<ClassificacaoRisco>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

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

        public async Task<CustomResponse<IList<ClassificacaoRisco>>> ConsultaClassificacaoRiscoPorPessoaId(Guid pessoaId, Guid userId)
        {

            var _response = new CustomResponse<IList<ClassificacaoRisco>>();


            try
            {
                var classificacoes = await _contextKlinikos.ClassificacoesRisco.Where(x => x.PessoaPaciente.PessoaId == pessoaId && x.Ativo).ToListAsync();
                _response.StatusCode = StatusCodes.Status200OK;
                _response.Result = classificacoes;
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

using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Dominio
{
    public class GrupoMedicamentoService : BaseService<GrupoMedicamento>, IGrupoMedicamentoService
    {

        // private IGrupoMedicamentoHistoricoService _serviceGrupoMedicamentoHistorico;
        private readonly DominioDbContext _contextDominio;

        public GrupoMedicamentoService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {
            _contextDominio = contextDominio;
           // _serviceGrupoMedicamentoHistorico = new GrupoMedicamentoHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<GrupoMedicamento>> AdicionarGrupoMedicamento(GrupoMedicamento grupoMedicamento, Guid userId)
        {
            var _response = new CustomResponse<GrupoMedicamento>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextDominio.Pessoas.Where(x => x.Master).FirstOrDefault();


                grupoMedicamento.Ativo = true;

                await this.Adicionar(grupoMedicamento, userId);

               // await _serviceGrupoMedicamentoHistorico.AdicionarHistoricoGrupoMedicamento(grupoMedicamento, _pessoaMaster);

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

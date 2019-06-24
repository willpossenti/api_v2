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
    public class AtendimentoMedicoPrescricaoReceitaDetalheService : BaseService<AtendimentoMedicoPrescricaoReceitaDetalhe>, IAtendimentoMedicoPrescricaoReceitaDetalheService
    {

        private readonly IAtendimentoMedicoPrescricaoReceitaDetalheHistoricoService _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoPrescricaoReceitaDetalheService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico = new AtendimentoMedicoPrescricaoReceitaDetalheHistoricoService(contextDominio, contextKlinikos, context);
        }

        public async Task<CustomResponse<AtendimentoMedicoPrescricaoReceitaDetalhe>> AdicionarAtendimentoMedicoPrescricaoReceitaDetalhe(AtendimentoMedicoPrescricaoReceitaDetalhe atendimentoMedicoPrescricaoReceitaDetalhe, Guid userId)
        {
            var _response = new CustomResponse<AtendimentoMedicoPrescricaoReceitaDetalhe>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                atendimentoMedicoPrescricaoReceitaDetalhe.Ativo = true;

                await this.Adicionar(atendimentoMedicoPrescricaoReceitaDetalhe, userId);

                await _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico.AdicionarHistoricoAtendimentoMedicoPrescricaoReceitaDetalhe(atendimentoMedicoPrescricaoReceitaDetalhe, _pessoaMaster);

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

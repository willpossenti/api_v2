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
    public class AtendimentoMedicoPrescricaoReceitaService : BaseService<AtendimentoMedicoPrescricaoReceita>, IAtendimentoMedicoPrescricaoReceitaService
    {

        private IAtendimentoMedicoPrescricaoReceitaHistoricoService _serviceAtendimentoMedicoPrescricaoReceitaHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoPrescricaoReceitaService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoPrescricaoReceitaHistorico = new AtendimentoMedicoPrescricaoReceitaHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<AtendimentoMedicoPrescricaoReceita>> AdicionarAtendimentoMedicoPrescricaoReceita(AtendimentoMedicoPrescricaoReceita atendimentoMedicoPrescricaoReceita, Guid userId)
        {
            var _response = new CustomResponse<AtendimentoMedicoPrescricaoReceita>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                atendimentoMedicoPrescricaoReceita.Ativo = true;

                await this.Adicionar(atendimentoMedicoPrescricaoReceita, userId);

                await _serviceAtendimentoMedicoPrescricaoReceitaHistorico.AdicionarHistoricoAtendimentoMedicoPrescricaoReceita(atendimentoMedicoPrescricaoReceita, _pessoaMaster);

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

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
    public class AtendimentoMedicoService : BaseService<AtendimentoMedico>, IAtendimentoMedicoService
    {

        private readonly IAtendimentoMedicoHistoricoService _serviceAtendimentoMedicoHistorico;
        private readonly IAtendimentoMedicoAlergiaHistoricoService _serviceAtendimentoMedicoAlergiaHistorico;
        private readonly IAtendimentoMedicoExameHistoricoService _serviceAtendimentoMedicoExameHistorico;
        private readonly IAtendimentoMedicoPrescricaoReceitaDetalheHistoricoService _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAtendimentoMedicoHistorico = new AtendimentoMedicoHistoricoService(contextDominio, contextKlinikos, context);
            _serviceAtendimentoMedicoAlergiaHistorico = new AtendimentoMedicoAlergiaHistoricoService(contextDominio, contextKlinikos, context);
            _serviceAtendimentoMedicoExameHistorico = new AtendimentoMedicoExameHistoricoService(contextDominio, contextKlinikos, context);
            _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico = new AtendimentoMedicoPrescricaoReceitaDetalheHistoricoService(contextDominio, contextKlinikos, context);

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

                if (atendimentoMedico.AtendimentoMedicoAlergia.Count > 0) {


                    foreach (var alergia in atendimentoMedico.AtendimentoMedicoAlergia) {

                        await _serviceAtendimentoMedicoAlergiaHistorico.AdicionarHistoricoAtendimentoMedicoAlergia(alergia, _pessoaMaster);
                    }

                }

                if (atendimentoMedico.AtendimentoMedicoExame.Count > 0)
                {

                    foreach (var exame in atendimentoMedico.AtendimentoMedicoExame)
                    {
                        await _serviceAtendimentoMedicoExameHistorico.AdicionarHistoricoAtendimentoMedicoExame(exame, _pessoaMaster);
                    }
                }

                if (atendimentoMedico.AtendimentoMedicoPrescricaoReceitaDetalhe.Count > 0)
                {

                    foreach (var prescricaoReceita in atendimentoMedico.AtendimentoMedicoPrescricaoReceitaDetalhe)
                    {
                        await _serviceAtendimentoMedicoPrescricaoReceitaDetalheHistorico.AdicionarHistoricoAtendimentoMedicoPrescricaoReceitaDetalhe(prescricaoReceita, _pessoaMaster);
                    }
                }
                        



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

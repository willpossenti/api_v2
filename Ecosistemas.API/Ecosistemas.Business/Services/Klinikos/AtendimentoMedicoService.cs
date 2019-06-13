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

<<<<<<< HEAD
        private readonly IAtendimentoMedicoHistoricoService _serviceAtendimentoMedicoHistorico;
=======
        private IAtendimentoMedicoHistoricoService _serviceAtendimentoMedicoHistorico;
        private IAtendimentoMedicoAlergiaHistoricoService _serviceAtendimentoMedicoAlergiaHistorico;
        private IAtendimentoMedicoExameHistoricoService _serviceAtendimentoMedicoExameHistorico;
        private IAtendimentoMedicoPrescricaoReceitaHistoricoService _serviceAtendimentoMedicoPrescricaoReceitaHistorico;
>>>>>>> sprint_yl_25052019
        private readonly KlinikosDbContext _contextKlinikos;

        public AtendimentoMedicoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
<<<<<<< HEAD
            _serviceAtendimentoMedicoHistorico = new AtendimentoMedicoHistoricoService(contextDominio, contextKlinikos, context);
=======
            _serviceAtendimentoMedicoHistorico = new AtendimentoMedicoHistoricoService(contextKlinikos, context);
            _serviceAtendimentoMedicoAlergiaHistorico = new AtendimentoMedicoAlergiaHistoricoService(contextKlinikos, context);
            _serviceAtendimentoMedicoExameHistorico = new AtendimentoMedicoExameHistoricoService(contextKlinikos, context);
            _serviceAtendimentoMedicoPrescricaoReceitaHistorico = new AtendimentoMedicoPrescricaoReceitaHistoricoService(contextKlinikos, context);
>>>>>>> sprint_yl_25052019
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

                if (atendimentoMedico.AtendimentoMedicoPrescricaoReceita.Count > 0)
                {

                    foreach (var prescricaoReceita in atendimentoMedico.AtendimentoMedicoPrescricaoReceita)
                    {
                        await _serviceAtendimentoMedicoPrescricaoReceitaHistorico.AdicionarHistoricoAtendimentoMedicoPrescricaoReceita(prescricaoReceita, _pessoaMaster);
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

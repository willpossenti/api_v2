using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class AtendimentoMedicoExameHistoricoService : BaseService<AtendimentoMedicoExameHistorico>, IAtendimentoMedicoExameHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public AtendimentoMedicoExameHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoExame(AtendimentoMedicoExame atendimentoMedicoExame, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoExameHistorico = new AtendimentoMedicoExameHistorico
                {
                    AtendimentoMedicoExame = atendimentoMedicoExame,
                    GrupoExame = atendimentoMedicoExame.GrupoExame?.Nome,
                    Exame = atendimentoMedicoExame.Exame?.Nome,
                    ObservacaoExame = atendimentoMedicoExame.ObservacaoExame,
                    DataExame = atendimentoMedicoExame.DataExame,
                    Profissional = atendimentoMedicoExame.Profissional?.NomeCompleto,
                    Ativo = atendimentoMedicoExame.Ativo,
                };

                await base.Adicionar(_AtendimentoMedicoExameHistorico, pessoaProfissionalCadastro.PessoaId);


                return _response;
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
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
    public class AtendimentoMedicoAlergiaHistoricoService : BaseService<AtendimentoMedicoAlergiaHistorico>, IAtendimentoMedicoAlergiaHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public AtendimentoMedicoAlergiaHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoAlergia(AtendimentoMedicoAlergia atendimentoMedicoAlergia, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoAlergiaHistorico = new AtendimentoMedicoAlergiaHistorico
                {
                    AtendimentoMedicoAlergia = atendimentoMedicoAlergia,
                    Alergia = atendimentoMedicoAlergia.Alergia?.Nome,
                    TipoAlergia = atendimentoMedicoAlergia.TipoAlergia?.Descricao,
                    LocalizacaoAlergia = atendimentoMedicoAlergia.LocalizacaoAlergia?.Nome,
                    ReacaoAlergia = atendimentoMedicoAlergia.ReacaoAlergia?.Descricao,
                    SeveridadeAlergia = atendimentoMedicoAlergia.SeveridadeAlergia?.Nome,
                    AlergiaSituacao = atendimentoMedicoAlergia.AlergiaSituacao,
                    DataSintomas = atendimentoMedicoAlergia.DataSintomas,
                    Ativo = atendimentoMedicoAlergia.Ativo,
                };

                await base.Adicionar(_AtendimentoMedicoAlergiaHistorico, pessoaProfissionalCadastro.PessoaId);


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
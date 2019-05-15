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
    public class AtendimentoMedicoPrescricaoReceitaHistoricoService : BaseService<AtendimentoMedicoPrescricaoReceitaHistorico>, IAtendimentoMedicoPrescricaoReceitaHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public AtendimentoMedicoPrescricaoReceitaHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoPrescricaoReceita(AtendimentoMedicoPrescricaoReceita atendimentoMedicoPrescricaoReceita, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoPrescricaoReceitaHistorico = new AtendimentoMedicoPrescricaoReceitaHistorico
                {
                    AtendimentoMedicoPrescricaoReceita = atendimentoMedicoPrescricaoReceita,
                    Dose = atendimentoMedicoPrescricaoReceita.Dose,
                    GrupoMedicamento = atendimentoMedicoPrescricaoReceita.GrupoMedicamento?.Nome,
                    Medicamento = atendimentoMedicoPrescricaoReceita.Medicamento?.Nome,
                    ViaAdministracaoMedicamento = atendimentoMedicoPrescricaoReceita.ViaAdministracaoMedicamento?.Descricao,
                    IntervaloMedicamento = atendimentoMedicoPrescricaoReceita.IntervaloMedicamento?.Descricao,
                    UnidadeMedicamento = atendimentoMedicoPrescricaoReceita.UnidadeMedicamento?.Descricao,
                    Observacao = atendimentoMedicoPrescricaoReceita?.Observacao,
                    Prescricao = atendimentoMedicoPrescricaoReceita.Prescricao,
                    Receita = atendimentoMedicoPrescricaoReceita.Receita,
                    Ativo = atendimentoMedicoPrescricaoReceita.Ativo,
                };

                await base.Adicionar(_AtendimentoMedicoPrescricaoReceitaHistorico, pessoaProfissionalCadastro.PessoaId);


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
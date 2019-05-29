using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
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
        private readonly DominioDbContext _contextDominio;

        public AtendimentoMedicoPrescricaoReceitaHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

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
                    GrupoMedicamento = _contextDominio.GruposMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.GrupoMedicamentoId).Result.Nome,
                    Medicamento = _contextDominio.Medicamentos.FindAsync(atendimentoMedicoPrescricaoReceita.MedicamentoId).Result.Nome,
                    ViaAdministracaoMedicamento = _contextDominio.ViasAdministracaoMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.ViaAdministracaoMedicamentoId).Result.Descricao,
                    IntervaloMedicamento = _contextDominio.IntervalosMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.IntervaloMedicamentoId).Result.Descricao,
                    UnidadeMedicamento = _contextDominio.UnidadesMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.UnidadeMedicamentoId).Result.Descricao,
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
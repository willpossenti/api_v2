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
                    Observacao = atendimentoMedicoPrescricaoReceita?.Observacao,
                    Prescricao = atendimentoMedicoPrescricaoReceita.Prescricao,
                    Receita = atendimentoMedicoPrescricaoReceita.Receita,
                    Ativo = atendimentoMedicoPrescricaoReceita.Ativo,
                };


                if (atendimentoMedicoPrescricaoReceita.GrupoMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaHistorico.GrupoMedicamento = _contextDominio.GruposMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.GrupoMedicamentoId).Result.Nome;

                if (atendimentoMedicoPrescricaoReceita.MedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaHistorico.Medicamento = _contextDominio.Medicamentos.FindAsync(atendimentoMedicoPrescricaoReceita.MedicamentoId).Result.Nome;

                if (atendimentoMedicoPrescricaoReceita.ViaAdministracaoMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaHistorico.ViaAdministracaoMedicamento = _contextDominio.ViasAdministracaoMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.ViaAdministracaoMedicamentoId).Result.Descricao;

                if (atendimentoMedicoPrescricaoReceita.IntervaloMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaHistorico.IntervaloMedicamento = _contextDominio.IntervalosMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.IntervaloMedicamentoId).Result.Descricao;


                if (atendimentoMedicoPrescricaoReceita.UnidadeMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaHistorico.UnidadeMedicamento = _contextDominio.UnidadesMedicamento.FindAsync(atendimentoMedicoPrescricaoReceita.UnidadeMedicamentoId).Result.Descricao;



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
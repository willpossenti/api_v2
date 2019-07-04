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
    public class AtendimentoMedicoPrescricaoReceitaDetalheHistoricoService : BaseService<AtendimentoMedicoPrescricaoReceitaDetalheHistorico>, IAtendimentoMedicoPrescricaoReceitaDetalheHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public AtendimentoMedicoPrescricaoReceitaDetalheHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoPrescricaoReceitaDetalhe(AtendimentoMedicoPrescricaoReceitaDetalhe atendimentoMedicoPrescricaoReceitaDetalhe, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoPrescricaoReceitaDetalheHistorico = new AtendimentoMedicoPrescricaoReceitaDetalheHistorico
                {
                    AtendimentoMedicoPrescricaoReceitaDetalhe = atendimentoMedicoPrescricaoReceitaDetalhe,
                    GrupoMedicamento = atendimentoMedicoPrescricaoReceitaDetalhe.GrupoMedicamento?.GrupoMedicamentoDetalhe?.Nome,                   
                    Dose = atendimentoMedicoPrescricaoReceitaDetalhe.Dose,
                    Observacao = atendimentoMedicoPrescricaoReceitaDetalhe.Observacao,
                    Prescricao = atendimentoMedicoPrescricaoReceitaDetalhe.Prescricao,
                    Receita = atendimentoMedicoPrescricaoReceitaDetalhe.Receita,
                    Ativo = atendimentoMedicoPrescricaoReceitaDetalhe.Ativo,
                };


                //if (atendimentoMedicoPrescricaoReceitaDetalhe.GrupoMedicamentoId != Guid.Empty)
                //    _AtendimentoMedicoPrescricaoReceitaDetalheHistorico.GrupoMedicamentoDetalhe = _contextDominio.GruposMedicamento.FindAsync(atendimentoMedicoPrescricaoReceitaDetalhe.GrupoMedicamentoId).Result.GrupoMedicamentoDetalhe;

                if (atendimentoMedicoPrescricaoReceitaDetalhe.MedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaDetalheHistorico.Medicamento = _contextDominio.Medicamentos.FindAsync(atendimentoMedicoPrescricaoReceitaDetalhe.MedicamentoId).Result.Nome;

                if (atendimentoMedicoPrescricaoReceitaDetalhe.ViaAdministracaoMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaDetalheHistorico.ViaAdministracaoMedicamento = _contextDominio.ViasAdministracaoMedicamento.FindAsync(atendimentoMedicoPrescricaoReceitaDetalhe.ViaAdministracaoMedicamentoId).Result.Descricao;

                if (atendimentoMedicoPrescricaoReceitaDetalhe.IntervaloMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaDetalheHistorico.IntervaloMedicamento = _contextDominio.IntervalosMedicamento.FindAsync(atendimentoMedicoPrescricaoReceitaDetalhe.IntervaloMedicamentoId).Result.Descricao;


                if (atendimentoMedicoPrescricaoReceitaDetalhe.UnidadeMedicamentoId != Guid.Empty)
                    _AtendimentoMedicoPrescricaoReceitaDetalheHistorico.UnidadeMedicamento = _contextDominio.UnidadesMedicamento.FindAsync(atendimentoMedicoPrescricaoReceitaDetalhe.UnidadeMedicamentoId).Result.Descricao;



                await base.Adicionar(_AtendimentoMedicoPrescricaoReceitaDetalheHistorico, pessoaProfissionalCadastro.PessoaId);


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
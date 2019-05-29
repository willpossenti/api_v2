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
    public class AtendimentoMedicoAlergiaHistoricoService : BaseService<AtendimentoMedicoAlergiaHistorico>, IAtendimentoMedicoAlergiaHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public AtendimentoMedicoAlergiaHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoAlergia(AtendimentoMedicoAlergia atendimentoMedicoAlergia, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoAlergiaHistorico = new AtendimentoMedicoAlergiaHistorico
                {
                    AtendimentoMedicoAlergia = atendimentoMedicoAlergia,
                    Alergia = _contextDominio.Alergias.FindAsync(atendimentoMedicoAlergia.AlergiaId).Result.Nome,
                    TipoAlergia = _contextDominio.TiposAlergia.FindAsync(atendimentoMedicoAlergia.TipoAlergiaId).Result.Descricao,
                    LocalizacaoAlergia = _contextDominio.LocalizacoesAlergia.FindAsync(atendimentoMedicoAlergia.LocalizacaoAlergiaId).Result.Nome,
                    ReacaoAlergia = _contextDominio.ReacoesAlergia.FindAsync(atendimentoMedicoAlergia.ReacaoAlergiaId).Result.Descricao,
                    SeveridadeAlergia = _contextDominio.SeveridadesAlergia.FindAsync(atendimentoMedicoAlergia.SeveridadeAlergiaId).Result.Nome,
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
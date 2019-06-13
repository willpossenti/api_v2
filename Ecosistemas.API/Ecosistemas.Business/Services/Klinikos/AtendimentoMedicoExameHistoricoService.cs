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
    public class AtendimentoMedicoExameHistoricoService : BaseService<AtendimentoMedicoExameHistorico>, IAtendimentoMedicoExameHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public AtendimentoMedicoExameHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoExame(AtendimentoMedicoExame atendimentoMedicoExame, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoExameHistorico = new AtendimentoMedicoExameHistorico
                {
                    AtendimentoMedicoExame = atendimentoMedicoExame,

                    //ExameLaboratorial = _contextDominio.Exames.FindAsync(atendimentoMedicoExame.AlergiaId).Result.Nome,
                    //ExameImagem = atendimentoMedicoExame.ExameImagem,

                    ObservacaoExame = atendimentoMedicoExame.ObservacaoExame,
                    DataExame = atendimentoMedicoExame.DataExame,
                    Profissional = atendimentoMedicoExame.Profissional?.NomeCompleto,
                    Ativo = atendimentoMedicoExame.Ativo,
                };

                if (atendimentoMedicoExame.GrupoExameId != Guid.Empty)
                    _AtendimentoMedicoExameHistorico.GrupoExame = _contextDominio.GruposExame.FindAsync(atendimentoMedicoExame.GrupoExameId).Result.Nome;

                if (atendimentoMedicoExame.ExameId != Guid.Empty)
                    _AtendimentoMedicoExameHistorico.Exame = _contextDominio.Exames.FindAsync(atendimentoMedicoExame.ExameId).Result.Nome;

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
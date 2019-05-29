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
    public class ClassificacaoRiscoAlergiaHistoricoService : BaseService<ClassificacaoRiscoAlergiaHistorico>, IClassificacaoRiscoAlergiaHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public ClassificacaoRiscoAlergiaHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoClassificacaoRiscoAlergia(ClassificacaoRiscoAlergia classificacaoRiscoAlergia, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _ClassificacaoRiscoAlergiaHistorico = new ClassificacaoRiscoAlergiaHistorico
                {
                    ClassificacaoRiscoAlergia = classificacaoRiscoAlergia,
                    Alergia = _contextDominio.Alergias.FindAsync(classificacaoRiscoAlergia.AlergiaId).Result.Nome,
                    TipoAlergia = _contextDominio.TiposAlergia.FindAsync(classificacaoRiscoAlergia.TipoAlergiaId).Result.Descricao,
                    LocalizacaoAlergia = _contextDominio.LocalizacoesAlergia.FindAsync(classificacaoRiscoAlergia.LocalizacaoAlergiaId).Result.Nome,
                    ReacaoAlergia = _contextDominio.ReacoesAlergia.FindAsync(classificacaoRiscoAlergia.ReacaoAlergiaId).Result.Descricao,
                    SeveridadeAlergia = _contextDominio.SeveridadesAlergia.FindAsync(classificacaoRiscoAlergia.SeveridadeAlergiaId).Result.Nome,
                    AlergiaSituacao = classificacaoRiscoAlergia.AlergiaSituacao,
                    DataSintomas = classificacaoRiscoAlergia.DataSintomas,
                    Ativo = classificacaoRiscoAlergia.Ativo,
                };

                await base.Adicionar(_ClassificacaoRiscoAlergiaHistorico, pessoaProfissionalCadastro.PessoaId);


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
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
    public class ClassificacaoRiscoAlergiaHistoricoService : BaseService<ClassificacaoRiscoAlergiaHistorico>, IClassificacaoRiscoAlergiaHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public ClassificacaoRiscoAlergiaHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoClassificacaoRiscoAlergia(ClassificacaoRiscoAlergia classificacaoRiscoAlergia, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _ClassificacaoRiscoAlergiaHistorico = new ClassificacaoRiscoAlergiaHistorico
                {
                    ClassificacaoRiscoAlergia = classificacaoRiscoAlergia,
                    Alergia = classificacaoRiscoAlergia.Alergia?.Nome,
                    TipoAlergia = classificacaoRiscoAlergia.TipoAlergia?.Descricao,
                    LocalizacaoAlergia = classificacaoRiscoAlergia.LocalizacaoAlergia?.Nome,
                    ReacaoAlergia = classificacaoRiscoAlergia.ReacaoAlergia?.Descricao,
                    SeveridadeAlergia = classificacaoRiscoAlergia.SeveridadeAlergia?.Nome,
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
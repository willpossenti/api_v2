using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IClassificacaoRiscoHistoricoService : IBaseService<ClassificacaoRiscoHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoClassificacaoRisco(ClassificacaoRisco classificacaoRisco, PessoaProfissional pessoaProfissionalCadastro);

    }
}

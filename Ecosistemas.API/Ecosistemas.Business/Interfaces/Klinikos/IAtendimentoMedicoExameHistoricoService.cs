using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoExameHistoricoService : IBaseService<AtendimentoMedicoExameHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoExame(AtendimentoMedicoExame atendimentoMedicoExame, PessoaProfissional pessoaProfissionalCadastro);


    }
}

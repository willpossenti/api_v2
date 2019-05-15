using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoHistoricoService : IBaseService<AtendimentoMedicoHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedico(AtendimentoMedico atendimentoMedico, PessoaProfissional pessoaProfissionalCadastro);

    }
}

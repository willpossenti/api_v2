using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
      public interface IPessoaHistoricoService : IBaseService<PessoaHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoPaciente(PessoaPaciente pessoaPaciente, PessoaProfissional pessoaProfissional);
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoProfissional(PessoaProfissional pessoaProfissional, PessoaProfissional pessoaProfissionalCadastro);
    }
}

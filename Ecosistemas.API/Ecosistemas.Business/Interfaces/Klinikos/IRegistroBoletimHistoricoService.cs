using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
      public interface IRegistroBoletimHistoricoService : IBaseService<RegistroBoletimHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoRegistroBoletim(RegistroBoletim registroBoletim, PessoaProfissional pessoaProfissionalCadastro);

    }
}

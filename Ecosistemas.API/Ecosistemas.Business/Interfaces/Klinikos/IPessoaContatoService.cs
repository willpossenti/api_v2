using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
      public interface IPessoaContatoService : IBaseService<PessoaContato>
    {
        Task<CustomResponse<List<PessoaContato>>> ConsultaContato(Guid pessoaId, Guid userId);
    }
}

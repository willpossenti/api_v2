using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
      public interface IPessoaService : IBaseService<Pessoa>
    {
         Task<CustomResponse<Pessoa>> ConsultaCpf(string cpf, Guid userId);
    }
}

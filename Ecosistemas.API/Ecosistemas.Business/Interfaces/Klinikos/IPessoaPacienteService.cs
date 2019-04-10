using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
      public interface IPessoaPacienteService : IBaseService<PessoaPaciente>
    {
        Task<CustomResponse<PessoaPaciente>> AdicionarPaciente(PessoaPaciente entity, Guid UserId);
        Task<CustomResponse<PessoaPaciente>> ConsultaCpf(string cpf);
        Task<CustomResponse<List<PessoaPaciente>>> ConsultarNome(string Nome);
        Task<CustomResponse<List<PessoaPaciente>>> ConsultarNomeComRaca(string Nome);
    }
   
 
}

        
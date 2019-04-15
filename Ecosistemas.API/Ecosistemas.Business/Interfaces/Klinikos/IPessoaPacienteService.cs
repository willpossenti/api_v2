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
        Task<CustomResponse<PessoaPaciente>> AtualizarPaciente(PessoaPaciente entity, Guid UserId);
        Task<CustomResponse<PessoaPaciente>> ConsultaCpf(string cpf, Guid userId);
        Task<CustomResponse<PessoaPaciente>> ConsultaCns(string cns, Guid userId);
        Task<CustomResponse<PessoaPaciente>> ConsultaPis(string pis, Guid userId);
        Task<CustomResponse<List<PessoaPaciente>>> ConsultaNome(string nome, Guid userId);
        Task<CustomResponse<List<PessoaPaciente>>> ConsultaNomeSocial(string nome, Guid userId);

    }
}

using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Api
{
    public interface IClienteService
    {
        Task<CustomResponse<Cliente>> Adicionar(Cliente sistema, Guid UserId);

        Task<CustomResponse<Cliente>> Atualizar(Cliente sistema, Guid UserId);

        Task<CustomResponse<Cliente>> Remover(Guid Id, Guid UserId);

        Task<CustomResponse<IList<Cliente>>> ListarTodos();

        Task<CustomResponse<Cliente>> Obter(Guid id);
    }
}

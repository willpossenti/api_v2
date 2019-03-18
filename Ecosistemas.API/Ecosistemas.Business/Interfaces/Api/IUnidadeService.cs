using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Api
{
    public interface IUnidadeService
    {
        Task<CustomResponse<Unidade>> Adicionar(Unidade sistema, Guid UserId);

        Task<CustomResponse<Unidade>> Atualizar(Unidade sistema, Guid UserId);

        Task<CustomResponse<Unidade>> Remover(Guid Id, Guid UserId);

        Task<CustomResponse<IList<Unidade>>> ListarTodos();

        Task<CustomResponse<Unidade>> Obter(Guid id);
    }
}

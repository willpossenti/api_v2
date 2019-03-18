using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Api
{
    public interface ISistemaService
    {
        Task<CustomResponse<Sistema>> Adicionar(Sistema sistema, Guid UserId);

        Task<CustomResponse<Sistema>> Atualizar(Sistema sistema, Guid UserId);

        Task<CustomResponse<Sistema>> Remover(Guid Id, Guid UserId);

        Task<CustomResponse<IList<Sistema>>> ListarTodos();

        Task<CustomResponse<Sistema>> Obter(Guid id);
    }
}

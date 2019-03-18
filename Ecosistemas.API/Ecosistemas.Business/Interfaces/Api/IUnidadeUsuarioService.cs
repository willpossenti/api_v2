using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Api
{
    public interface IUnidadeUsuarioService
    {
        Task<CustomResponse<UnidadeUsuario>> Adicionar(UnidadeUsuario sistema, Guid UserId);

        Task<CustomResponse<UnidadeUsuario>> Atualizar(UnidadeUsuario sistema, Guid UserId);

        Task<CustomResponse<UnidadeUsuario>> Remover(Guid Id, Guid UserId);

        Task<CustomResponse<IList<UnidadeUsuario>>> ListarTodos();

        Task<CustomResponse<UnidadeUsuario>> Obter(Guid id);
    }
}

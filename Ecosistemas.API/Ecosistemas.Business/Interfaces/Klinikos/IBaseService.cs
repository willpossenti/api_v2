using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IBaseService<T> where T : class
    {
        Task<CustomResponse<T>> Adicionar(T entity, Guid UserId);
        Task<CustomResponse<T>> AdicionarRange(List<T> entity, Guid UserId);
        Task<CustomResponse<T>> Remover(Guid Id, Guid UserId);
        Task<CustomResponse<T>> Atualizar(T entity, Guid UserId);
        Task<CustomResponse<IList<T>>> ListarTodos();
        Task<CustomResponse<T>> Obter(Guid id);
        Task<CustomResponse<IList<T>>> ObterByExpression(Expression<Func<T, bool>> predicate);
        //Task<CustomResponse<T>> GerarLog(string action, string entity, Guid UserId);
        CustomResponse<T> AdicionarCarga(List<T> entity, Guid UserId);

        void Dispose();
    }
}

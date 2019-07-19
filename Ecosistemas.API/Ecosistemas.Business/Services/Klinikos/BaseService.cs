using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Contexto.Api;
using System.Reflection;
using System.Linq.Expressions;
using System.Transactions;

namespace Ecosistemas.Business.Services.Klinikos
{

    public class BaseService<T> : IDisposable, IBaseService<T> where T : class
    {
        private KlinikosDbContext _contextKlinikos;
        private ApiDbContext _context;

        public BaseService(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;
        }

        public async Task<CustomResponse<T>> Adicionar(T entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _contextKlinikos.Attach<T>(entity);
                await _contextKlinikos.SaveChangesAsync();
                _response.Message = "Inclusão";
                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = entity;
                await GerarLog(_response.Message, typeof(T).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
            return _response;
        }

        public async Task<CustomResponse<T>> AdicionarRange(List<T> entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _contextKlinikos.AttachRange(entity);
                await _contextKlinikos.SaveChangesAsync();
                _response.Message = "Inclusão de lista";
                _response.StatusCode = StatusCodes.Status201Created;
                await GerarLog(_response.Message, typeof(T).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }

            return _response;
        }

        public CustomResponse<T> AdicionarCarga(List<T> entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _contextKlinikos.AddRange(entity);
                _contextKlinikos.SaveChanges();
                _response.Message = "Inclusão de lista";
                _response.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }


            return _response;
        }


        public async Task<CustomResponse<T>> Atualizar(T entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _contextKlinikos.Update<T>(entity).State = EntityState.Modified;
                await _contextKlinikos.SaveChangesAsync();
                _response.Message = "Alteração";
                _response.StatusCode = StatusCodes.Status200OK;
                await GerarLog(_response.Message, typeof(T).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
            return _response;
        }

        public async Task<CustomResponse<T>> Remover(Guid Id, Guid UserId)
        {
            var _response = new CustomResponse<T>();
            try
            {
                T _entity = _contextKlinikos.Set<T>().Find(Id);
                _contextKlinikos.Remove<T>(_entity);
                await _contextKlinikos.SaveChangesAsync();

                _response.Message = "Remoção";
                _response.StatusCode = StatusCodes.Status200OK;

                await GerarLog(_response.Message, typeof(T).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }

            return _response;
        }

        public async Task<CustomResponse<IList<T>>> ListarTodos()
        {
            var _response = new CustomResponse<IList<T>>();

            try
            {
                _response.Result = await _contextKlinikos.Set<T>().ToListAsync<T>();
                _response.Message = "Sucesso";
                _response.StatusCode = StatusCodes.Status302Found;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<T>> Obter(Guid id)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _response.Result = await _contextKlinikos.FindAsync<T>(id);
                _response.Message = "Sucesso";
                _response.StatusCode = StatusCodes.Status302Found;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
            return _response;
        }

        public async Task<CustomResponse<IList<T>>> ObterByExpression(Expression<Func<T, bool>> predicate)
        {
            var _response = new CustomResponse<IList<T>> ();

            try
            {
                _response.Result = await _contextKlinikos.Set<T>().Where(predicate).ToListAsync();
                _response.Message = "Sucesso";
                _response.StatusCode = StatusCodes.Status302Found;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
            return _response;
        }



        public async Task<CustomResponse<T>> GerarLog(string action, string entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();


            try
            {

                    var _log = new Log
                    {
                        Acao = action,
                        Data = DateTime.Now,
                        LocalAcao = entity.ToString(),
                        User = await _context.FindAsync<User>(UserId)
                    };

                await _context.AddAsync<Log>(_log);
                await _context.SaveChangesAsync();
                _response.StatusCode = StatusCodes.Status201Created;
                
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.StatusCode = StatusCodes.Status417ExpectationFailed;
                Error.LogError(ex);
            }
            return _response;
        }


        public void Dispose()
        {
            _contextKlinikos.Dispose();
            _context.Dispose();
        }


    }

}

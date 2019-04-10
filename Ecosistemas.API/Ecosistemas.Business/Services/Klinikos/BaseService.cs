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

namespace Ecosistemas.Business.Services.Klinikos
{

    public class BaseService<T> : IDisposable, IBaseService<T> where T : class
    {
        private KlinikosDbContext _context;

        public BaseService(KlinikosDbContext context)
        {
            _context = context;

        }

        public async Task<CustomResponse<T>> Adicionar(T entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _context.Attach<T>(entity);
                await _context.SaveChangesAsync();
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
                _context.AttachRange(entity);
                await _context.SaveChangesAsync();
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
                _context.AddRange(entity);
                _context.SaveChanges();
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
                _context.Update<T>(entity);
                await _context.SaveChangesAsync();
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
                T _entity = _context.Set<T>().Find(Id);
                _context.Remove<T>(_entity);
                await _context.SaveChangesAsync();

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
                _response.Result = await _context.Set<T>().ToListAsync<T>();
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
                _response.Result = await _context.FindAsync<T>(id);
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
                _response.Result = await _context.Set<T>().Where(predicate).ToListAsync();
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
                using (ApiDbContext _apicontext = new ApiDbContext())
                {
                    var _log = new Log
                    {
                        Acao = action,
                        Data = DateTime.Now,
                        LocalAcao = entity.ToString(),
                        User = await _apicontext.FindAsync<User>(UserId)
                    };

                    await _apicontext.AddAsync<Log>(_log);
                    await _apicontext.SaveChangesAsync();
                    _response.StatusCode = StatusCodes.Status201Created;
                }
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
            _context.Dispose();
        }


    }

}

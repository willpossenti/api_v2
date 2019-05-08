using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Utility;

namespace Ecosistemas.Business.Services.Api
{

    public class BaseService<T>: IDisposable, IBaseService<T> where T : class
    {
        private readonly ApiDbContext _context;

        public BaseService(ApiDbContext context)
        {
            _context = context;

        }

        public async Task<CustomResponse<T>> Adicionar(T entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                await _context.AddAsync<T>(entity);
                await _context.SaveChangesAsync();
                _response.Message = "Inclusão";
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

        public CustomResponse<T> AdicionarCarga(T entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            try
            {
                _context.AddAsync<T>(entity);
                _context.SaveChangesAsync();
                _response.Message = "Inclusão";
                _response.StatusCode = StatusCodes.Status201Created;

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
                await _context.AddRangeAsync(entity);
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

        public async Task<CustomResponse<T>> GerarLog(string action, string entity, Guid UserId)
        {
            var _response = new CustomResponse<T>();

            var _log = new Log
            {
                Acao = action,
                Data = DateTime.Now,
                LocalAcao = entity.ToString(),
                User = await _context.FindAsync<User>(UserId)
            };

            try
            {
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
            throw new NotImplementedException();
        }
    }
}

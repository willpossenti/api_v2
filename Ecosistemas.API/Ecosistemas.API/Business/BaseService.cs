using Ecosistemas.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecosistemas.API.Utility;
using Microsoft.AspNetCore.Http;
using Ecosistemas.API.Model;

namespace Ecosistemas.API.Business
{
    
    public class BaseService<TEntity> where TEntity : class
    {

        private CatalogoDbContext _context;

        public BaseService(CatalogoDbContext context)
        {
            _context = context;
            
        }

        public async Task<CustomResponse<TEntity>> Incluir(TEntity entity, Guid UserId)
        {
            var _response = new CustomResponse<TEntity>();

            _context.Add<TEntity>(entity);

            try
            {
                await _context.SaveChangesAsync();
                _response.Message =  "Inclusão";
                _response.StatusCode = StatusCodes.Status201Created;

                await GerarLog(_response.Message, typeof(TEntity).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                
            }

            return _response;
        }

        public async Task<CustomResponse<TEntity>> IncluirRange(List<TEntity> entity, Guid UserId)
        {
            var _response = new CustomResponse<TEntity>();
            _context.AddRange(entity);

            try
            {
                await _context.SaveChangesAsync();
                _response.Message = "Inclusão de lista";
                _response.StatusCode = StatusCodes.Status201Created;
                await GerarLog(_response.Message, typeof(TEntity).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;

            }

            return _response;
        }

        public async Task<CustomResponse<TEntity>> Alterar(TEntity entity, Guid UserId)
        {
            var _response = new CustomResponse<TEntity>();

            _context.Update<TEntity>(entity);

            try
            {
                await _context.SaveChangesAsync();
                _response.Message = "Alteração";
                _response.StatusCode = StatusCodes.Status200OK;

                await GerarLog(_response.Message, typeof(TEntity).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }


            return _response;
        }

        public async Task<CustomResponse<TEntity>> Remover(Guid Id, Guid UserId)
        {
            var _response = new CustomResponse<TEntity>();


            try
            {
                TEntity _entity = _context.Set<TEntity>().Find(Id);
                _context.Remove<TEntity>(_entity);

                await _context.SaveChangesAsync();

                _response.Message = "Remoção";
                _response.StatusCode = StatusCodes.Status200OK;

                await GerarLog(_response.Message, typeof(TEntity).Name, UserId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CustomResponse<IList<TEntity>>> BuscarTodos()
        {
            var _response = new CustomResponse<IList<TEntity>>();

            try
            {
                _response.Result =  await _context.Set<TEntity>().ToListAsync<TEntity>();
                _response.Message = "Sucesso";
                _response.StatusCode = StatusCodes.Status302Found;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CustomResponse<TEntity>> Buscar(Guid id) 
        {
            var _response = new CustomResponse<TEntity>();

            try
            {
                _response.Result = await _context.FindAsync<TEntity>(id);
                _response.Message = "Sucesso";
                _response.StatusCode = StatusCodes.Status302Found;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CustomResponse<Log>> GerarLog(string action, string entity, Guid UserId) {

            var _response = new CustomResponse<Log>();

            var _log = new Log
            {
                Acao = action,
                Data = DateTime.Now,
                LocalAcao = entity.ToString(),
                User = await _context.FindAsync<User>(UserId)
            };

            _context.Add<Log>(_log);

            try
            {
                await _context.SaveChangesAsync();
                _response.StatusCode = StatusCodes.Status201Created;

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.StatusCode = StatusCodes.Status417ExpectationFailed;
            }

            return _response;
        }

    }
}

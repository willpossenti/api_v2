using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Dominio
{
    public class CidadeService : BaseService<Cidade>, ICidadeService
    {
        private readonly DominioDbContext _contextDominio;
        private readonly ApiDbContext _context;

        public CidadeService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {
            _context = context;
            _contextDominio = contextDominio;

        }


        public async Task<CustomResponse<IList<Cidade>>> GetByEstado(Guid estadoId)
        {
            var _response = new CustomResponse<IList<Cidade>>();

            try
            {

                _response.Result = await _contextDominio.Set<Cidade>().Include(e => e.Estado).Where(x => x.Estado.EstadoId == estadoId)
                    .Select(s => new Cidade { CidadeId = s.CidadeId, Nome = s.Nome, Ativo = s.Ativo }).ToListAsync();

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
        public async Task<CustomResponse<IList<Cidade>>> GetByName(string nome)
        {
            var _response = new CustomResponse<IList<Cidade>>();

            try
            {
                Expression<Func<Cidade, bool>> filtroNome = x => x.Nome.Contains(nome);
                return await base.ObterByExpression(filtroNome);
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

    }
}
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.Business.Services.Dominio
{
    public class EventoService : BaseService<Evento>, IEventoService
    {
        public EventoService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {


        }

        public async Task<CustomResponse<Evento>> GetBySigla(string sigla)
        {
            var _response = new CustomResponse<Evento>();

            try
            {
                Expression<Func<Evento, bool>> filtroSigla = x => x.Sigla.Contains(sigla);
                var _eventos = await base.ObterByExpression(filtroSigla);
                _response.StatusCode = StatusCodes.Status302Found;
                _response.Result = _eventos.Result.FirstOrDefault();

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


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Dominio
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class EventoController : Controller
    {
        private readonly IEventoService _service;

        public EventoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new EventoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Evento>> Incluir([FromBody]Evento evento)
        {
            return await _service.Adicionar(evento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Evento>> Put([FromBody]Evento evento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(evento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EventoId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Evento>> Delete(string EventoId)
        {
            return await _service.Remover(Guid.Parse(EventoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Evento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{EventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Evento>> Get(string EventoId)
        {
            return await _service.Obter(Guid.Parse(EventoId));
        }

        [HttpGet("GetByDescricao/{descricao}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Evento>> GetByDescricao(string descricao)
        {
            return await _service.GetByDescricao(descricao);
        }



    }
}

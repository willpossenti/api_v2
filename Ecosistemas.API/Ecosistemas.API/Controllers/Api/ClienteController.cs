

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Services.Api;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;

        public ClienteController(ApiDbContext context)
        {
            _service = new ClienteService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Cliente>> Incluir([FromBody]Cliente cliente)
        {
            return _service.Adicionar(cliente, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Cliente>> Put([FromBody]Cliente cliente)
        {
            return _service.Atualizar(cliente, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ClienteId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Cliente>> Delete(string ClienteId)
        {
            return _service.Remover(Guid.Parse(ClienteId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<Cliente>>> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{ClienteId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<Cliente>> Get(string ClienteId)
        {
            return _service.Obter(Guid.Parse(ClienteId));
        }

      

    }
}

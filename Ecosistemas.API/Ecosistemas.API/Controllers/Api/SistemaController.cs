

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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class SistemaController : Controller
    {
        private readonly ISistemaService _service;

        public SistemaController(ApiDbContext context)
        {
            _service = new SistemaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Sistema>> Incluir([FromBody]Sistema sistema)
        {
            return _service.Adicionar(sistema, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Sistema>> Put([FromBody]Sistema sistema)
        {
            return _service.Atualizar(sistema, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{SistemaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Sistema>> Delete(string SistemaId)
        {
            return _service.Remover(Guid.Parse(SistemaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<Sistema>>> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{SistemaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<Sistema>> Get(string SistemaId)
        {
            return _service.Obter(Guid.Parse(SistemaId));
        }

      

    }
}

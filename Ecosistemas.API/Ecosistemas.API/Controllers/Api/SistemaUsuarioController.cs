

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
    public class UnidadeUsuarioController : Controller
    {
        private IUnidadeUsuarioService _service;

        public UnidadeUsuarioController(ApiDbContext context)
        {
            _service = new UnidadeUsuarioService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<UnidadeUsuario>> Incluir([FromBody]UnidadeUsuario usuarioSistema)
        {
            return _service.Adicionar(usuarioSistema, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<UnidadeUsuario>> Put([FromBody]UnidadeUsuario usuarioSistema)
        {
            return _service.Atualizar(usuarioSistema, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ClienteId}")]
        //[Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<UnidadeUsuario>> Delete(string UnidadeUsuarioId)
        {
            return _service.Remover(Guid.Parse(UnidadeUsuarioId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<UnidadeUsuario>>> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{ClienteId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<UnidadeUsuario>> Get(string UnidadeUsuarioId)
        {
            return _service.Obter(Guid.Parse(UnidadeUsuarioId));
        }

      

    }
}

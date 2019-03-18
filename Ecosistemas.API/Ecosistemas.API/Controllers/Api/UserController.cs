

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
    public class UserController : Controller
    {
        private IUserService _service;

        public UserController(ApiDbContext context)
        {
            _service = new UserService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Incluir([FromBody]User user, [FromServices]AccessManager accessManager)
        {
            return _service.Adicionar(user, accessManager, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Put([FromBody]User user, [FromServices]AccessManager accessManager)
        {
            return _service.Atualizar(user, accessManager, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{UserId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Delete(string UserId)
        {
            return _service.RemoverUser(Guid.Parse(UserId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<User>>> Get()
        {
            return _service.BuscarTodosUsers();
        }

       

        [Route("Confirmarsenha")]
        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<User>> Confirmarsenha([FromBody]User user, [FromServices]AccessManager accessManager)
        {
            return _service.ConfirmarSenha(user, accessManager, Guid.Parse(HttpContext.User.Identity.Name));
        }


    }
}

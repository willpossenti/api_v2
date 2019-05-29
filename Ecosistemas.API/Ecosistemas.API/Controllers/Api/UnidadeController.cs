

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
    public class UnidadeController : Controller
    {
        private readonly IUnidadeService _service;

        public UnidadeController(ApiDbContext context)
        {
            _service = new UnidadeService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Unidade>> Incluir([FromBody]Unidade unidade)
        {
            return _service.Adicionar(unidade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Unidade>> Put([FromBody]Unidade unidade)
        {
            return _service.Atualizar(unidade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{UnidadeId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<Unidade>> Delete(string UnidadeId)
        {
            return _service.Remover(Guid.Parse(UnidadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<Unidade>>> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{UnidadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<Unidade>> Get(string UnidadeId)
        {
            return _service.Obter(Guid.Parse(UnidadeId));
        }

      

    }
}

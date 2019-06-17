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
    public class AberturaOcularController : Controller
    {
        private readonly IAberturaOcularService _service;

        public AberturaOcularController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new AberturaOcularService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<AberturaOcular>> Incluir([FromBody]AberturaOcular aberturaOcular)
        {
            return await _service.Adicionar(aberturaOcular, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<AberturaOcular>> Put([FromBody]AberturaOcular aberturaOcular, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(aberturaOcular, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AberturaOcularId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<AberturaOcular>> Delete(string AberturaOcularId)
        {
            return await _service.Remover(Guid.Parse(AberturaOcularId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<AberturaOcular>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AberturaOcularId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AberturaOcular>> Get(string AberturaOcularId)
        {
            return await _service.Obter(Guid.Parse(AberturaOcularId));
        }



    }
}

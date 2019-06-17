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
    public class RiscoController : Controller
    {
        private readonly IRiscoService _service;

        public RiscoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new RiscoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Risco>> Incluir([FromBody]Risco risco)
        {
            return await _service.Adicionar(risco, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Risco>> Put([FromBody]Risco risco, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(risco, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RiscoId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Risco>> Delete(string RiscoId)
        {
            return await _service.Remover(Guid.Parse(RiscoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Risco>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{RiscoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Risco>> Get(string RiscoId)
        {
            return await _service.Obter(Guid.Parse(RiscoId));
        }



    }
}

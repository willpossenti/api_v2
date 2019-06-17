

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
    public class PreferencialController : Controller
    {
        private readonly IPreferencialService _service;

        public PreferencialController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new PreferencialService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Preferencial>> Incluir([FromBody]Preferencial preferencial)
        {
            return await _service.Adicionar(preferencial, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Preferencial>> Put([FromBody]Preferencial preferencial, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(preferencial, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{PreferencialId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Preferencial>> Delete(string PreferencialId)
        {
            return await _service.Remover(Guid.Parse(PreferencialId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Preferencial>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{PreferencialId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Preferencial>> Get(string PreferencialId)
        {
            return await _service.Obter(Guid.Parse(PreferencialId));
        }

      

    }
}

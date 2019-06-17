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
    public class EscalaDorController : Controller
    {
        private readonly IEscalaDorService _service;

        public EscalaDorController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new EscalaDorService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<EscalaDor>> Incluir([FromBody]EscalaDor escalador)
        {
            return await _service.Adicionar(escalador, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<EscalaDor>> Put([FromBody]EscalaDor escalador, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(escalador, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EscalaDorId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<EscalaDor>> Delete(string EscalaDorId)
        {
            return await _service.Remover(Guid.Parse(EscalaDorId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<EscalaDor>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{EscalaDorId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<EscalaDor>> Get(string EscalaDorId)
        {
            return await _service.Obter(Guid.Parse(EscalaDorId));
        }



    }
}

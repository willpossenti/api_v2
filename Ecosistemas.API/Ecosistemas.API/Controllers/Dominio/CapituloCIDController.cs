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
    public class CapituloCIDController : Controller
    {
        private readonly ICapituloCIDService _service;

        public CapituloCIDController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new CapituloCIDService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<CapituloCID>> Incluir([FromBody]CapituloCID capituloCID)
        {
            return await _service.Adicionar(capituloCID, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<CapituloCID>> Put([FromBody]CapituloCID capituloCID, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(capituloCID, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{CapituloCIDId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<CapituloCID>> Delete(string CapituloCIDId)
        {
            return await _service.Remover(Guid.Parse(CapituloCIDId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<CapituloCID>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{CapituloCIDId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CapituloCID>> Get(string CapituloCIDId)
        {
            return await _service.Obter(Guid.Parse(CapituloCIDId));
        }



    }
}

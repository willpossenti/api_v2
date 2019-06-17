

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
    public class JustificativaController : Controller
    {
        private readonly IJustificativaService _service;

        public JustificativaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new JustificativaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Justificativa>> Incluir([FromBody]Justificativa justificativa)
        {
            return await _service.Adicionar(justificativa, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Justificativa>> Put([FromBody]Justificativa justificativa, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(justificativa, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{JustificativaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Justificativa>> Delete(string JustificativaId)
        {
            return await _service.Remover(Guid.Parse(JustificativaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Justificativa>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{JustificativaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Justificativa>> Get(string JustificativaId)
        {
            return await _service.Obter(Guid.Parse(JustificativaId));
        }

      

    }
}

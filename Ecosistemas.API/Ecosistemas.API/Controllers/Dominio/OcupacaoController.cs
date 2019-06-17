

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
    public class OcupacaoController : Controller
    {
        private readonly IOcupacaoService _service;

        public OcupacaoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new OcupacaoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Ocupacao>> Incluir([FromBody]Ocupacao ocupacao)
        {
            return await _service.Adicionar(ocupacao, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Ocupacao>> Put([FromBody]Ocupacao ocupacao, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(ocupacao, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{OcupacaoId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Ocupacao>> Delete(string OcupacaoId)
        {
            return await _service.Remover(Guid.Parse(OcupacaoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Ocupacao>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{OcupacaoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Ocupacao>> Get(string OcupacaoId)
        {
            return await _service.Obter(Guid.Parse(OcupacaoId));
        }

      

    }
}

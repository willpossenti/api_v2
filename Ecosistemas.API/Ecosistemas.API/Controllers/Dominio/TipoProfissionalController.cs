

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
    public class TipoProfissionalController : Controller
    {
        private readonly ITipoProfissionalService _service;

        public TipoProfissionalController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new TipoProfissionalService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoProfissional>> Incluir([FromBody]TipoProfissional tipoProfissional)
        {
            return await _service.Adicionar(tipoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoProfissional>> Put([FromBody]TipoProfissional tipoProfissional, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoProfissionalId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoProfissional>> Delete(string TipoProfissionalId)
        {
            return await _service.Remover(Guid.Parse(TipoProfissionalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoProfissional>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoProfissionalId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoProfissional>> Get(string TipoProfissionalId)
        {
            return await _service.Obter(Guid.Parse(TipoProfissionalId));
        }

      

    }
}

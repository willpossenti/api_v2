

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
    public class TipoCertidaoController : Controller
    {
        private readonly ITipoCertidaoService _service;

        public TipoCertidaoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new TipoCertidaoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoCertidao>> Incluir([FromBody]TipoCertidao pais)
        {
            return await _service.Adicionar(pais, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoCertidao>> Put([FromBody]TipoCertidao pais, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(pais, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoCertidaoId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoCertidao>> Delete(string TipoCertidaoId)
        {
            return await _service.Remover(Guid.Parse(TipoCertidaoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoCertidao>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoCertidaoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoCertidao>> Get(string TipoCertidaoId)
        {
            return await _service.Obter(Guid.Parse(TipoCertidaoId));
        }

      

    }
}

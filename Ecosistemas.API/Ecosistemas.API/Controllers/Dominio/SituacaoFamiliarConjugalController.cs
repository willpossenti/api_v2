

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
    public class SituacaoFamiliarConjugalController : Controller
    {
        private readonly ISituacaoFamiliarConjugalService _service;

        public SituacaoFamiliarConjugalController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new SituacaoFamiliarConjugalService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SituacaoFamiliarConjugal>> Incluir([FromBody]SituacaoFamiliarConjugal situacaofamiliarconjugal)
        {
            return await _service.Adicionar(situacaofamiliarconjugal, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SituacaoFamiliarConjugal>> Put([FromBody]SituacaoFamiliarConjugal situacaofamiliarconjugal, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(situacaofamiliarconjugal, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{SituacaoFamiliarConjugalId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SituacaoFamiliarConjugal>> Delete(string SituacaoFamiliarConjugalId)
        {
            return await _service.Remover(Guid.Parse(SituacaoFamiliarConjugalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<SituacaoFamiliarConjugal>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{SituacaoFamiliarConjugalId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SituacaoFamiliarConjugal>> Get(string SituacaoFamiliarConjugalId)
        {
            return await _service.Obter(Guid.Parse(SituacaoFamiliarConjugalId));
        }

      

    }
}

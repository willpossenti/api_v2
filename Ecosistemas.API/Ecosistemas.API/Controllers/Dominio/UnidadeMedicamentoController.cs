using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UnidadeMedicamentoController : Controller
    {
        private readonly IUnidadeMedicamentoService _service;

        public UnidadeMedicamentoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new UnidadeMedicamentoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<UnidadeMedicamento>> Incluir([FromBody]UnidadeMedicamento unidadeMedicamento)
        {
            return await _service.Adicionar(unidadeMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<UnidadeMedicamento>> Put([FromBody]UnidadeMedicamento unidadeMedicamento)
        {
            return await _service.Atualizar(unidadeMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{UnidadeMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<UnidadeMedicamento>> Delete(string UnidadeMedicamentoId)
        {
            return await _service.Remover(Guid.Parse(UnidadeMedicamentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<UnidadeMedicamento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{UnidadeMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<UnidadeMedicamento>> Get(string UnidadeMedicamentoId)
        {
            return await _service.Obter(Guid.Parse(UnidadeMedicamentoId));
        }



    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Services.Klinikos;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UnidadeMedicamentoController : Controller
    {
        private readonly IUnidadeMedicamentoService _service;

        public UnidadeMedicamentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new UnidadeMedicamentoService(contextKlinikos, context);
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
        public async Task<CustomResponse<UnidadeMedicamento>> Put([FromBody]UnidadeMedicamento unidadeMedicamento, [FromServices]AccessManager accessManager)
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

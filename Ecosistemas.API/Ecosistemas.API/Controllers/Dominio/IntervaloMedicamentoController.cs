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

namespace Ecosistemas.API.Controllers.Dominio
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class IntervaloMedicamentoController : Controller
    {
        private readonly IIntervaloMedicamentoService _service;

        public IntervaloMedicamentoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new IntervaloMedicamentoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IntervaloMedicamento>> Incluir([FromBody]IntervaloMedicamento intervaloMedicamento)
        {
            return await _service.Adicionar(intervaloMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IntervaloMedicamento>> Put([FromBody]IntervaloMedicamento intervaloMedicamento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(intervaloMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{IntervaloMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IntervaloMedicamento>> Delete(string IntervaloMedicamentoId)
        {
            return await _service.Remover(Guid.Parse(IntervaloMedicamentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<IntervaloMedicamento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{IntervaloMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IntervaloMedicamento>> Get(string IntervaloMedicamentoId)
        {
            return await _service.Obter(Guid.Parse(IntervaloMedicamentoId));
        }



    }
}

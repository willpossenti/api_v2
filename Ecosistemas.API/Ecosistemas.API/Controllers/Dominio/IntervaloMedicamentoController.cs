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
    public class IntervaloMedicamentoController : Controller
    {
        private readonly IIntervaloMedicamentoService _service;

        public IntervaloMedicamentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new IntervaloMedicamentoService(contextKlinikos, context);
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

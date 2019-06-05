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
    public class MedicamentoController : Controller
    {
        private readonly IMedicamentoService _service;

        public MedicamentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new MedicamentoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Medicamento>> Incluir([FromBody]Medicamento medicamento)
        {
            return await _service.Adicionar(medicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Medicamento>> Put([FromBody]Medicamento medicamento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(medicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{MedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Medicamento>> Delete(string MedicamentoId)
        {
            return await _service.Remover(Guid.Parse(MedicamentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Medicamento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{MedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Medicamento>> Get(string MedicamentoId)
        {
            return await _service.Obter(Guid.Parse(MedicamentoId));
        }

        [HttpGet("ConsultaMedicamento/{nome}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<Medicamento>>> ConsultaMedicamento(string nome)
        {
            return await _service.ConsultaMedicamento(nome, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

    }
}

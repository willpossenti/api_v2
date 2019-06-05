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
    public class ViaAdministracaoMedicamentoController : Controller
    {
        private readonly IViaAdministracaoMedicamentoService _service;

        public ViaAdministracaoMedicamentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ViaAdministracaoMedicamentoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ViaAdministracaoMedicamento>> Incluir([FromBody]ViaAdministracaoMedicamento viaAdministracaoMedicamento)
        {
            return await _service.Adicionar(viaAdministracaoMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ViaAdministracaoMedicamento>> Put([FromBody]ViaAdministracaoMedicamento viaAdministracaoMedicamento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(viaAdministracaoMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ViaAdministracaoMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ViaAdministracaoMedicamento>> Delete(string ViaAdministracaoMedicamentoId)
        {
            return await _service.Remover(Guid.Parse(ViaAdministracaoMedicamentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ViaAdministracaoMedicamento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ViaAdministracaoMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ViaAdministracaoMedicamento>> Get(string ViaAdministracaoMedicamentoId)
        {
            return await _service.Obter(Guid.Parse(ViaAdministracaoMedicamentoId));
        }



    }
}

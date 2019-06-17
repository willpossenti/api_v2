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
    public class ViaAdministracaoMedicamentoController : Controller
    {
        private readonly IViaAdministracaoMedicamentoService _service;

        public ViaAdministracaoMedicamentoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new ViaAdministracaoMedicamentoService(contextDominio, context);
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
        public async Task<CustomResponse<ViaAdministracaoMedicamento>> Put([FromBody]ViaAdministracaoMedicamento viaAdministracaoMedicamento)
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

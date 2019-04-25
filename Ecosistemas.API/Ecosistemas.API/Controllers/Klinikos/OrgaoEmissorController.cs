

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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrgaoEmissorController : Controller
    {
        private readonly IOrgaoEmissorService _service;

        public OrgaoEmissorController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new OrgaoEmissorService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<OrgaoEmissor>> Incluir([FromBody]OrgaoEmissor orgaoemissor)
        {
            return await _service.Adicionar(orgaoemissor, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<OrgaoEmissor>> Put([FromBody]OrgaoEmissor orgaoemissor, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(orgaoemissor, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{OrgaoEmissorId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<OrgaoEmissor>> Delete(string OrgaoEmissorId)
        {
            return await _service.Remover(Guid.Parse(OrgaoEmissorId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<OrgaoEmissor>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{OrgaoEmissorId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<OrgaoEmissor>> Get(string OrgaoEmissorId)
        {
            return await _service.Obter(Guid.Parse(OrgaoEmissorId));
        }

      

    }
}

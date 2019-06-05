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
    public class ConsultaCIDController : Controller
    {
        private readonly IConsultaCIDService _service;

        public ConsultaCIDController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ConsultaCIDService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ConsultaCID>> Incluir([FromBody]ConsultaCID consultaCID)
        {
            return await _service.Adicionar(consultaCID, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ConsultaCID>> Put([FromBody]ConsultaCID consultaCID, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(consultaCID, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ConsultaCIDId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ConsultaCID>> Delete(string ConsultaCIDId)
        {
            return await _service.Remover(Guid.Parse(ConsultaCIDId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ConsultaCID>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ConsultaCIDId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ConsultaCID>> Get(string ConsultaCIDId)
        {
            return await _service.Obter(Guid.Parse(ConsultaCIDId));
        }



    }
}

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
    public class EscalaDorController : Controller
    {
        private readonly IEscalaDorService _service;

        public EscalaDorController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new EscalaDorService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<EscalaDor>> Incluir([FromBody]EscalaDor escalador)
        {
            return await _service.Adicionar(escalador, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<EscalaDor>> Put([FromBody]EscalaDor escalador, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(escalador, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EscalaDorId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<EscalaDor>> Delete(string EscalaDorId)
        {
            return await _service.Remover(Guid.Parse(EscalaDorId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<EscalaDor>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{EscalaDorId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<EscalaDor>> Get(string EscalaDorId)
        {
            return await _service.Obter(Guid.Parse(EscalaDorId));
        }



    }
}



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
    //[Authorize("Bearer")]
    public class RegistroBoletimController : Controller
    {
        private readonly IRegistroBoletimService _service;

        public RegistroBoletimController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new RegistroBoletimService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RegistroBoletim>> Incluir([FromBody]RegistroBoletim registroBoletim)
        {
            return await _service.AdicionarRegistroBoletim(registroBoletim, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RegistroBoletim>> Put([FromBody]RegistroBoletim registroBoletim, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(registroBoletim, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RegistroBoletimId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RegistroBoletim>> Delete(string registroBoletimId)
        {
            return await _service.Remover(Guid.Parse(registroBoletimId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<RegistroBoletim>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{RegistroBoletimId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RegistroBoletim>> Get(string registroBoletimId)
        {
            return await _service.Obter(Guid.Parse(registroBoletimId));
        }

      

    }
}

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
    public class SeveridadeAlergiaController : Controller
    {
        private readonly ISeveridadeAlergiaService _service;

        public SeveridadeAlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new SeveridadeAlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SeveridadeAlergia>> Incluir([FromBody]SeveridadeAlergia severidadeAlergia)
        {
            return await _service.Adicionar(severidadeAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SeveridadeAlergia>> Put([FromBody]SeveridadeAlergia severidadeAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(severidadeAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{SeveridadeAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SeveridadeAlergia>> Delete(string SeveridadeAlergiaId)
        {
            return await _service.Remover(Guid.Parse(SeveridadeAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<SeveridadeAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{SeveridadeAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SeveridadeAlergia>> Get(string SeveridadeAlergiaId)
        {
            return await _service.Obter(Guid.Parse(SeveridadeAlergiaId));
        }



    }
}

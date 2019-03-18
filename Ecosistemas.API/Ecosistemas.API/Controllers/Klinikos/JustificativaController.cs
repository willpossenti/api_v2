

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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class JustificativaController : Controller
    {
        private IJustificativaService _service;

        public JustificativaController(KlinikosDbContext context)
        {
            _service = new JustificativaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Justificativa>> Incluir([FromBody]Justificativa justificativa)
        {
            return await _service.Adicionar(justificativa, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Justificativa>> Put([FromBody]Justificativa justificativa, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(justificativa, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{JustificativaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Justificativa>> Delete(string JustificativaId)
        {
            return await _service.Remover(Guid.Parse(JustificativaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Justificativa>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{JustificativaId}")]
       // [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Justificativa>> Get(string JustificativaId)
        {
            return await _service.Obter(Guid.Parse(JustificativaId));
        }

      

    }
}

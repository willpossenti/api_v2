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
    public class AtendimentoMedicoAlergiaController : Controller
    {
        private readonly IAtendimentoMedicoAlergiaService _service;


        public AtendimentoMedicoAlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AtendimentoMedicoAlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoAlergia>> Incluir([FromBody]AtendimentoMedicoAlergia atendimentoMedicoAlergia)
        {
            return await _service.AdicionarAtendimentoMedicoAlergia(atendimentoMedicoAlergia, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoAlergia>> Put([FromBody]AtendimentoMedicoAlergia atendimentoMedicoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(atendimentoMedicoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AtendimentoMedicoAlergiaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoAlergia>> Delete(string AtendimentoMedicoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(AtendimentoMedicoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<AtendimentoMedicoAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AtendimentoMedicoAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoAlergia>> Get(string AtendimentoMedicoAlergiaId)
        {
            return await _service.Obter(Guid.Parse(AtendimentoMedicoAlergiaId));
        }



    }
}

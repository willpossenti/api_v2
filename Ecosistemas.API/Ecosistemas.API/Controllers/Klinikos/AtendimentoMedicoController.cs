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
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Services.Klinikos;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Microsoft.AspNetCore.Cors;


namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class AtendimentoMedicoController : Controller
    {
        private readonly IAtendimentoMedicoService _service;

        public AtendimentoMedicoController(DominioDbContext dominioDbContext, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AtendimentoMedicoService(dominioDbContext, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedico>> Incluir([FromBody]AtendimentoMedico atendimentoMedico)
        {
            return await _service.AdicionarAtendimentoMedico(atendimentoMedico, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedico>> Put([FromBody]AtendimentoMedico atendimentoMedico, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(atendimentoMedico, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AtendimentoMedicoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedico>> Delete(string AtendimentoMedicoId)
        {
            return await _service.Remover(Guid.Parse(AtendimentoMedicoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<AtendimentoMedico>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AtendimentoMedicoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedico>> Get(string AtendimentoMedicoId)
        {
            return await _service.Obter(Guid.Parse(AtendimentoMedicoId));
        }



    }
}

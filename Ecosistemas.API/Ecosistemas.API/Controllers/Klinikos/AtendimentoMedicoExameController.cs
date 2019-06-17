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
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoMedicoExameController : Controller
    {
        private readonly IAtendimentoMedicoExameService _service;

        public AtendimentoMedicoExameController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AtendimentoMedicoExameService(contextDominio, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoExame>> Incluir([FromBody]AtendimentoMedicoExame atendimentoMedicoExame)
        {
            return await _service.AdicionarAtendimentoMedicoExame(atendimentoMedicoExame, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoExame>> Put([FromBody]AtendimentoMedicoExame atendimentoMedicoExame, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(atendimentoMedicoExame, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AtendimentoMedicoExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoExame>> Delete(string AtendimentoMedicoExameId)
        {
            return await _service.Remover(Guid.Parse(AtendimentoMedicoExameId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<AtendimentoMedicoExame>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AtendimentoMedicoExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AtendimentoMedicoExame>> Get(string AtendimentoMedicoExameId)
        {
            return await _service.Obter(Guid.Parse(AtendimentoMedicoExameId));
        }



    }
}

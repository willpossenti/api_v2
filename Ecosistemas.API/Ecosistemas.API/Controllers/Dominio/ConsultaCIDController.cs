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
    public class ConsultaCIDController : Controller
    {
        private readonly IConsultaCIDService _service;

        public ConsultaCIDController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new ConsultaCIDService(contextDominio, context);
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
        public async Task<CustomResponse<ConsultaCID>> Put([FromBody]ConsultaCID consultaCID)
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

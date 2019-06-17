using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Dominio
{

    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class RespostaVerbalController : Controller
    {
        private readonly IRespostaVerbalService _service;

        public RespostaVerbalController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new RespostaVerbalService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaVerbal>> Incluir([FromBody]RespostaVerbal respostaVerbal)
        {
            return await _service.Adicionar(respostaVerbal, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaVerbal>> Put([FromBody]RespostaVerbal respostaVerbal, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(respostaVerbal, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RespostaVerbalId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaVerbal>> Delete(string RespostaVerbalId)
        {
            return await _service.Remover(Guid.Parse(RespostaVerbalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<RespostaVerbal>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{RespostaVerbalId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaVerbal>> Get(string RespostaVerbalId)
        {
            return await _service.Obter(Guid.Parse(RespostaVerbalId));
        }



    }
}

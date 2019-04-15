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

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    public class RespostaVerbalController : Controller
    {
        private IRespostaVerbalService _service;

        public RespostaVerbalController(KlinikosDbContext context)
        {
            _service = new RespostaVerbalService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaVerbal>> Incluir([FromBody]RespostaVerbal respostaVerbal)
        {
            return await _service.Adicionar(respostaVerbal, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaVerbal>> Put([FromBody]RespostaVerbal respostaVerbal, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(respostaVerbal, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RespostaVerbalId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaVerbal>> Delete(string RespostaVerbalId)
        {
            return await _service.Remover(Guid.Parse(RespostaVerbalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
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

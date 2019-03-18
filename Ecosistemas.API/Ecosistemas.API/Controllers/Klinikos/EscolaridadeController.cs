

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
    public class EscolaridadeController : Controller
    {
        private IEscolaridadeService _service;

        public EscolaridadeController(KlinikosDbContext context)
        {
            _service = new EscolaridadeService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Escolaridade>> Incluir([FromBody]Escolaridade escolaridade)
        {
            return await _service.Adicionar(escolaridade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Escolaridade>> Put([FromBody]Escolaridade escolaridade, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(escolaridade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EscolaridadeId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Escolaridade>> Delete(string EscolaridadeId)
        {
            return await _service.Remover(Guid.Parse(EscolaridadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Escolaridade>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{EscolaridadeId}")]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Escolaridade>> Get(string EscolaridadeId)
        {
            return await _service.Obter(Guid.Parse(EscolaridadeId));
        }

      

    }
}

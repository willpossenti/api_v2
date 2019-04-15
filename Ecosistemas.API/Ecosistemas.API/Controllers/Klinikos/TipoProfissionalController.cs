

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
    public class TipoProfissionalController : Controller
    {
        private ITipoProfissionalService _service;

        public TipoProfissionalController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new TipoProfissionalService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoProfissional>> Incluir([FromBody]TipoProfissional tipoProfissional)
        {
            return await _service.Adicionar(tipoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoProfissional>> Put([FromBody]TipoProfissional tipoProfissional, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoProfissionalId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoProfissional>> Delete(string TipoProfissionalId)
        {
            return await _service.Remover(Guid.Parse(TipoProfissionalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoProfissional>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoProfissionalId}")]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoProfissional>> Get(string TipoProfissionalId)
        {
            return await _service.Obter(Guid.Parse(TipoProfissionalId));
        }

      

    }
}

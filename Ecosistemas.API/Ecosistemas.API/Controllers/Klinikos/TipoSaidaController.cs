

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
    [Authorize("Bearer")]
    public class TipoSaidaController : Controller
    {
        private readonly ITipoSaidaService _service;

        public TipoSaidaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new TipoSaidaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoSaida>> Incluir([FromBody]TipoSaida tiposaida)
        {
            return await _service.Adicionar(tiposaida, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoSaida>> Put([FromBody]TipoSaida tiposaida, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tiposaida, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoSaidaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoSaida>> Delete(string TipoSaidaId)
        {
            return await _service.Remover(Guid.Parse(TipoSaidaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoSaida>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoSaidaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoSaida>> Get(string TipoSaidaId)
        {
            return await _service.Obter(Guid.Parse(TipoSaidaId));
        }



    }
}

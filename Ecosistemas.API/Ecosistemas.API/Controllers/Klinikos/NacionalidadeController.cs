

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
    public class NacionalidadeController : Controller
    {
        private readonly INacionalidadeService _service;

        public NacionalidadeController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new NacionalidadeService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Nacionalidade>> Incluir([FromBody]Nacionalidade nacionalidade)
        {
            return await _service.Adicionar(nacionalidade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Nacionalidade>> Put([FromBody]Nacionalidade nacionalidade, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(nacionalidade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{NacionalidadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Nacionalidade>> Delete(string NacionalidadeId)
        {
            return await _service.Remover(Guid.Parse(NacionalidadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Nacionalidade>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{NacionalidadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Nacionalidade>> Get(string NacionalidadeId)
        {
            return await _service.Obter(Guid.Parse(NacionalidadeId));
        }

     

    }
}

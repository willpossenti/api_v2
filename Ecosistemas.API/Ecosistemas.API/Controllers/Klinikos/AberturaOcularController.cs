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
    [Authorize("Bearer")]
    public class AberturaOcularController : Controller
    {
        private readonly IAberturaOcularService _service;

        public AberturaOcularController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AberturaOcularService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AberturaOcular>> Incluir([FromBody]AberturaOcular aberturaOcular)
        {
            return await _service.Adicionar(aberturaOcular, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AberturaOcular>> Put([FromBody]AberturaOcular aberturaOcular, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(aberturaOcular, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AberturaOcularId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AberturaOcular>> Delete(string AberturaOcularId)
        {
            return await _service.Remover(Guid.Parse(AberturaOcularId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<AberturaOcular>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AberturaOcularId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<AberturaOcular>> Get(string AberturaOcularId)
        {
            return await _service.Obter(Guid.Parse(AberturaOcularId));
        }



    }
}

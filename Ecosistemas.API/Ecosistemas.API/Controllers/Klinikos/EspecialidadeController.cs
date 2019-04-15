

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
    public class EspecialidadeController : Controller
    {
        private IEspecialidadeService _service;

        public EspecialidadeController(KlinikosDbContext context)
        {
            _service = new EspecialidadeService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Especialidade>> Incluir([FromBody]Especialidade especialidade)
        {
            return await _service.Adicionar(especialidade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Especialidade>> Put([FromBody]Especialidade especialidade, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(especialidade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EspecialidadeId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Especialidade>> Delete(string especialidadeId)
        {
            return await _service.Remover(Guid.Parse(especialidadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Especialidade>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{RacaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Especialidade>> Get(string especialidadeId)
        {
            return await _service.Obter(Guid.Parse(especialidadeId));
        }

      

    }
}

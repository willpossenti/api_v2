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
    public class AlergiaController : Controller
    {
        private readonly IAlergiaService _service;

        public AlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Alergia>> Incluir([FromBody]Alergia alergia)
        {
            return await _service.Adicionar(alergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Alergia>> Put([FromBody]Alergia alergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(alergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AlergiaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Alergia>> Delete(string AlergiaId)
        {
            return await _service.Remover(Guid.Parse(AlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Alergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Alergia>> Get(string AlergiaId)
        {
            return await _service.Obter(Guid.Parse(AlergiaId));
        }



    }
}

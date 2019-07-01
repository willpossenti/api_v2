

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
using Ecosistemas.Business.Contexto.Dominio;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class FilaRegistroController : Controller
    {
        private readonly IFilaRegistroService _service;

        public FilaRegistroController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new FilaRegistroService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistro>> Incluir([FromBody]FilaRegistro filaRegistro)
        {
            return await _service.Adicionar(filaRegistro, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistro>> Put([FromBody]FilaRegistro filaRegistro)
        {
            return await _service.Atualizar(filaRegistro, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{filaRegistroId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistro>> Delete(string filaRegistroId)
        {
            return await _service.Remover(Guid.Parse(filaRegistroId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaRegistro>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{filaRegistroId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistro>> Get(string filaRegistroId)
        {
            return await _service.Obter(Guid.Parse(filaRegistroId));
        }

      

    }
}

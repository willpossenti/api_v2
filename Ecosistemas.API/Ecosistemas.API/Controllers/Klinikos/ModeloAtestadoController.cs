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
    public class ModeloAtestadoController : Controller
    {
        private readonly IModeloAtestadoService _service;

        public ModeloAtestadoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ModeloAtestadoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloAtestado>> Incluir([FromBody]ModeloAtestado modeloAtestado)
        {
            return await _service.Adicionar(modeloAtestado, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloAtestado>> Put([FromBody]ModeloAtestado modeloAtestado, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(modeloAtestado, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ModeloAtestadoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloAtestado>> Delete(string ModeloAtestadoId)
        {
            return await _service.Remover(Guid.Parse(ModeloAtestadoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ModeloAtestado>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ModeloAtestadoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloAtestado>> Get(string ModeloAtestadoId)
        {
            return await _service.Obter(Guid.Parse(ModeloAtestadoId));
        }



    }
}

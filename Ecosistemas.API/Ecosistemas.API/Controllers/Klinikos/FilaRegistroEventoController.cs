

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
    public class FilaRegistroEventoController : Controller
    {
        private readonly IFilaRegistroEventoService _service;

        public FilaRegistroEventoController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new FilaRegistroEventoService(contextDominio, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> Incluir([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.Adicionar(filaregistroevento, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> Put([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.Atualizar(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{FilaRegistroEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> Delete(string FilaRegistroEventoId)
        {
            return await _service.Remover(Guid.Parse(FilaRegistroEventoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaRegistroEvento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{FilaRegistroEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> Get(string FilaRegistroEventoId)
        {
            return await _service.Obter(Guid.Parse(FilaRegistroEventoId));
        }

        [Route("ConsultarRegistrosNovos")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosNovos([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.ConsultarRegistrosNovos(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosRetirados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosRetirados([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.ConsultarRegistrosRetirados(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosChamadosAoPainel")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosChamadosAoPainel([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.ConsultarRegistrosChamadosAoPainel(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosCancelados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosCancelados([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.ConsultarRegistrosCancelados(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosConfirmados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosConfirmados([FromBody]FilaRegistroEvento filaregistroevento)
        {
            return await _service.ConsultarRegistrosConfirmados(filaregistroevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


    }
}

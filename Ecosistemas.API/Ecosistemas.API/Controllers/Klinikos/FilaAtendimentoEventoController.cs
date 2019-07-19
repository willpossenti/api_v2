

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
    public class FilaAtendimentoEventoController : Controller
    {
        private readonly IFilaAtendimentoEventoService _service;

        public FilaAtendimentoEventoController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new FilaAtendimentoEventoService(contextDominio, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> Incluir([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.Adicionar(filaatendimentoevento, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> Put([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.Atualizar(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{FilaAtendimentoEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> Delete(string FilaAtendimentoEventoId)
        {
            return await _service.Remover(Guid.Parse(FilaAtendimentoEventoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaAtendimentoEvento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{FilaRegistroEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> Get(string FilaAtendimentoEventoId)
        {
            return await _service.Obter(Guid.Parse(FilaAtendimentoEventoId));
        }

        [Route("ConsultarRegistrosNovos")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosNovos([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.ConsultarRegistrosNovos(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosRetirados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosRetirados([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.ConsultarRegistrosRetirados(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosChamadosAoPainel")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosChamadosAoPainel([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.ConsultarRegistrosChamadosAoPainel(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosCancelados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosCancelados([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.ConsultarRegistrosCancelados(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosConfirmados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosConfirmados([FromBody]FilaAtendimentoEvento filaatendimentoevento)
        {
            return await _service.ConsultarRegistrosConfirmados(filaatendimentoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


    }
}

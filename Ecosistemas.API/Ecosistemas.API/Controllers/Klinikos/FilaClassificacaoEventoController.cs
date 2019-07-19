

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
    public class FilaClassificacaoEventoController : Controller
    {
        private readonly IFilaClassificacaoEventoService _service;

        public FilaClassificacaoEventoController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new FilaClassificacaoEventoService(contextDominio, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> Incluir([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.Adicionar(filaclassificacaoevento, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> Put([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.Atualizar(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{FilaClassificacaoEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> Delete(string filaClassificacaoEventoId)
        {
            return await _service.Remover(Guid.Parse(filaClassificacaoEventoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaClassificacaoEvento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{FilaRegistroEventoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> Get(string filaClassificacaoEventoId)
        {
            return await _service.Obter(Guid.Parse(filaClassificacaoEventoId));
        }

        [Route("ConsultarRegistrosNovos")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosNovos([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.ConsultarRegistrosNovos(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosRetirados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosRetirados([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.ConsultarRegistrosRetirados(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosChamadosAoPainel")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosChamadosAoPainel([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.ConsultarRegistrosChamadosAoPainel(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosCancelados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosCancelados([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.ConsultarRegistrosCancelados(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("ConsultarRegistrosConfirmados")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosConfirmados([FromBody]FilaClassificacaoEvento filaclassificacaoevento)
        {
            return await _service.ConsultarRegistrosConfirmados(filaclassificacaoevento, Guid.Parse(HttpContext.User.Identity.Name));
        }


    }
}

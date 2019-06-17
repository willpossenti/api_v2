using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Dominio
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class NivelConscienciaController : Controller
    {
        private readonly INivelConscienciaService _service;

        public NivelConscienciaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new NivelConscienciaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<NivelConsciencia>> Incluir([FromBody]NivelConsciencia nivelConsciencia)
        {
            return await _service.Adicionar(nivelConsciencia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<NivelConsciencia>> Put([FromBody]NivelConsciencia nivelConsciencia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(nivelConsciencia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{NivelConscienciaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<NivelConsciencia>> Delete(string NivelConscienciaId)
        {
            return await _service.Remover(Guid.Parse(NivelConscienciaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<NivelConsciencia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{NivelConscienciaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<NivelConsciencia>> Get(string NivelConscienciaId)
        {
            return await _service.Obter(Guid.Parse(NivelConscienciaId));
        }



    }
}

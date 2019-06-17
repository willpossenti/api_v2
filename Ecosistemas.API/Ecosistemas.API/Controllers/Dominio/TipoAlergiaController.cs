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
    [ApiController]
    [EnableCors("ApiPolicy")]
    [Authorize("Bearer")]
    public class TipoAlergiaController : Controller
    {
        private readonly ITipoAlergiaService _service;

        public TipoAlergiaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new TipoAlergiaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoAlergia>> Incluir([FromBody]TipoAlergia tipoAlergia)
        {
            return await _service.Adicionar(tipoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoAlergia>> Put([FromBody]TipoAlergia tipoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoAlergiaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<TipoAlergia>> Delete(string TipoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(TipoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoAlergia>> Get(string TipoAlergiaId)
        {
            return await _service.Obter(Guid.Parse(TipoAlergiaId));
        }



    }
}

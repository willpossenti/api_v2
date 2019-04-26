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
    public class TipoAlergiaController : Controller
    {
        private readonly ITipoAlergiaService _service;

        public TipoAlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new TipoAlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoAlergia>> Incluir([FromBody]TipoAlergia tipoAlergia)
        {
            return await _service.Adicionar(tipoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoAlergia>> Put([FromBody]TipoAlergia tipoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoAlergiaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoAlergia>> Delete(string TipoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(TipoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
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

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
    public class LocalizacaoAlergiaController : Controller
    {
        private readonly ILocalizacaoAlergiaService _service;

        public LocalizacaoAlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new LocalizacaoAlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LocalizacaoAlergia>> Incluir([FromBody]LocalizacaoAlergia localizacaoAlergia)
        {
            return await _service.Adicionar(localizacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LocalizacaoAlergia>> Put([FromBody]LocalizacaoAlergia localizacaoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(localizacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{LocalizacaoAlergiaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LocalizacaoAlergia>> Delete(string LocalizacaoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(LocalizacaoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<LocalizacaoAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{LocalizacaoAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LocalizacaoAlergia>> Get(string LocalizacaoAlergiaId)
        {
            return await _service.Obter(Guid.Parse(LocalizacaoAlergiaId));
        }



    }
}

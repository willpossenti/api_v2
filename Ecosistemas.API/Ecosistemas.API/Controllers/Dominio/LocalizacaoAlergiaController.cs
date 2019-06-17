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
    public class LocalizacaoAlergiaController : Controller
    {
        private readonly ILocalizacaoAlergiaService _service;

        public LocalizacaoAlergiaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new LocalizacaoAlergiaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<LocalizacaoAlergia>> Incluir([FromBody]LocalizacaoAlergia localizacaoAlergia)
        {
            return await _service.Adicionar(localizacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<LocalizacaoAlergia>> Put([FromBody]LocalizacaoAlergia localizacaoAlergia)
        {
            return await _service.Atualizar(localizacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{LocalizacaoAlergiaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<LocalizacaoAlergia>> Delete(string LocalizacaoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(LocalizacaoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
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

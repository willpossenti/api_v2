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
    public class FilaStatusController : Controller
    {
        private readonly IFilaStatusService _service;

        public FilaStatusController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new FilaStatusService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<FilaStatus>> Incluir([FromBody]FilaStatus FilaStatus)
        {
            return await _service.Adicionar(FilaStatus, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<FilaStatus>> Put([FromBody]FilaStatus FilaStatus)
        {
            return await _service.Atualizar(FilaStatus, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{FilaStatusId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<FilaStatus>> Delete(string FilaStatusId)
        {
            return await _service.Remover(Guid.Parse(FilaStatusId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaStatus>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{FilaStatusId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaStatus>> Get(string FilaStatusId)
        {
            return await _service.Obter(Guid.Parse(FilaStatusId));
        }



    }
}

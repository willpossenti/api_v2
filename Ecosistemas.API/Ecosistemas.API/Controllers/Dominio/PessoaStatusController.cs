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
    public class PessoaStatusController : Controller
    {
        private readonly IPessoaStatusService _service;

        public PessoaStatusController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new PessoaStatusService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<PessoaStatus>> Incluir([FromBody]PessoaStatus PessoaStatus)
        {
            return await _service.Adicionar(PessoaStatus, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<PessoaStatus>> Put([FromBody]PessoaStatus PessoaStatus)
        {
            return await _service.Atualizar(PessoaStatus, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{PessoaStatusId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<PessoaStatus>> Delete(string PessoaStatusId)
        {
            return await _service.Remover(Guid.Parse(PessoaStatusId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<PessoaStatus>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{PessoaStatusId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaStatus>> Get(string PessoaStatusId)
        {
            return await _service.Obter(Guid.Parse(PessoaStatusId));
        }



    }
}

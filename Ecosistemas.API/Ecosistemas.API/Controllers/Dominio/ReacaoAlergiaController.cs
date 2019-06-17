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
    public class ReacaoAlergiaController : Controller
    {
        private readonly IReacaoAlergiaService _service;

        public ReacaoAlergiaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new ReacaoAlergiaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<ReacaoAlergia>> Incluir([FromBody]ReacaoAlergia reacaoAlergia)
        {
            return await _service.Adicionar(reacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<ReacaoAlergia>> Put([FromBody]ReacaoAlergia reacaoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(reacaoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ReacaoAlergiaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<ReacaoAlergia>> Delete(string ReacaoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(ReacaoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ReacaoAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ReacaoAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ReacaoAlergia>> Get(string AberturaOcularId)
        {
            return await _service.Obter(Guid.Parse(AberturaOcularId));
        }



    }
}

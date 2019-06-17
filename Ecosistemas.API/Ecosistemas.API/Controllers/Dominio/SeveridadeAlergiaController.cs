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
    public class SeveridadeAlergiaController : Controller
    {
        private readonly ISeveridadeAlergiaService _service;

        public SeveridadeAlergiaController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new SeveridadeAlergiaService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SeveridadeAlergia>> Incluir([FromBody]SeveridadeAlergia severidadeAlergia)
        {
            return await _service.Adicionar(severidadeAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SeveridadeAlergia>> Put([FromBody]SeveridadeAlergia severidadeAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(severidadeAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{SeveridadeAlergiaId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<SeveridadeAlergia>> Delete(string SeveridadeAlergiaId)
        {
            return await _service.Remover(Guid.Parse(SeveridadeAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<SeveridadeAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{SeveridadeAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<SeveridadeAlergia>> Get(string SeveridadeAlergiaId)
        {
            return await _service.Obter(Guid.Parse(SeveridadeAlergiaId));
        }



    }
}

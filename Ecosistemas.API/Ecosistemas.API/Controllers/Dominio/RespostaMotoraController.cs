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
    public class RespostaMotoraController : Controller
    {
        private readonly IRespostaMotoraService _service;

        public RespostaMotoraController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new RespostaMotoraService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaMotora>> Incluir([FromBody]RespostaMotora respostaMotora)
        {
            return await _service.Adicionar(respostaMotora, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaMotora>> Put([FromBody]RespostaMotora respostaMotora, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(respostaMotora, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RespostaMotoraId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<RespostaMotora>> Delete(string RespostaMotoraId)
        {
            return await _service.Remover(Guid.Parse(RespostaMotoraId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<RespostaMotora>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{RespostaMotoraId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaMotora>> Get(string RespostaMotoraId)
        {
            return await _service.Obter(Guid.Parse(RespostaMotoraId));
        }



    }
}

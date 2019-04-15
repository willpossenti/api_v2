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

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    public class RespostaMotoraController : Controller
    {
        private IRespostaMotoraService _service;

        public RespostaMotoraController(KlinikosDbContext context)
        {
            _service = new RespostaMotoraService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaMotora>> Incluir([FromBody]RespostaMotora respostaMotora)
        {
            return await _service.Adicionar(respostaMotora, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaMotora>> Put([FromBody]RespostaMotora respostaMotora, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(respostaMotora, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{RespostaMotoraId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<RespostaMotora>> Delete(string RespostaMotoraId)
        {
            return await _service.Remover(Guid.Parse(RespostaMotoraId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
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

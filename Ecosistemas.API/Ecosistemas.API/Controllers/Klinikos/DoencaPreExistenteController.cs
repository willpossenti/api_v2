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
    public class DoencaPreExistenteController : Controller
    {
        private IDoencaPreExistenteService _service;

        public DoencaPreExistenteController(KlinikosDbContext context)
        {
            _service = new DoencaPreExistenteService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<DoencaPreExistente>> Incluir([FromBody]DoencaPreExistente doencaPreExistente)
        {
            return await _service.Adicionar(doencaPreExistente, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<DoencaPreExistente>> Put([FromBody]DoencaPreExistente doencaPreExistente, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(doencaPreExistente, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{DoencaPreExistenteId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<DoencaPreExistente>> Delete(string DoencaPreExistenteId)
        {
            return await _service.Remover(Guid.Parse(DoencaPreExistenteId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<DoencaPreExistente>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{DoencaPreExistenteId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<DoencaPreExistente>> Get(string DoencaPreExistenteId)
        {
            return await _service.Obter(Guid.Parse(DoencaPreExistenteId));
        }


       
    }
}

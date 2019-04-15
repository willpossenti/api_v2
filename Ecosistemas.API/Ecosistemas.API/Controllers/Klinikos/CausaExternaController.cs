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
    public class CausaExternaController : Controller
    {
        private ICausaExternaService _service;

        public CausaExternaController(KlinikosDbContext context)
        {
            _service = new CausaExternaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CausaExterna>> Incluir([FromBody]CausaExterna causaExterna)
        {
            return await _service.Adicionar(causaExterna, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CausaExterna>> Put([FromBody]CausaExterna causaExterna, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(causaExterna, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{CausaExternaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CausaExterna>> Delete(string CausaExternaId)
        {
            return await _service.Remover(Guid.Parse(CausaExternaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<CausaExterna>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{CausaExternaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CausaExterna>> Get(string CausaExternaId)
        {
            return await _service.Obter(Guid.Parse(CausaExternaId));
        }



    }
}

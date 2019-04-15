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
    public class NivelConscienciaController : Controller
    {
        private INivelConscienciaService _service;

        public NivelConscienciaController(KlinikosDbContext context)
        {
            _service = new NivelConscienciaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<NivelConsciencia>> Incluir([FromBody]NivelConsciencia nivelConsciencia)
        {
            return await _service.Adicionar(nivelConsciencia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<NivelConsciencia>> Put([FromBody]NivelConsciencia nivelConsciencia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(nivelConsciencia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{NivelConscienciaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<NivelConsciencia>> Delete(string NivelConscienciaId)
        {
            return await _service.Remover(Guid.Parse(NivelConscienciaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<NivelConsciencia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{NivelConscienciaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<NivelConsciencia>> Get(string NivelConscienciaId)
        {
            return await _service.Obter(Guid.Parse(NivelConscienciaId));
        }



    }
}

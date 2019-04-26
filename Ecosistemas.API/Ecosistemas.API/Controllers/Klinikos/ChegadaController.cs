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
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.API.Controllers.Klinikos
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChegadaController : Controller
    {
        private readonly IChegadaService _service;

        public ChegadaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ChegadaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Chegada>> Incluir([FromBody]Chegada chegada)
        {
            return await _service.Adicionar(chegada, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Chegada>> Put([FromBody]Chegada chegada, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(chegada, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ChegadaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Chegada>> Delete(string ChegadaId)
        {
            return await _service.Remover(Guid.Parse(ChegadaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Chegada>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ChegadaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Chegada>> Get(string ChegadaId)
        {
            return await _service.Obter(Guid.Parse(ChegadaId));
        }



    }
}



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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CidadeController : Controller
    {
        private readonly ICidadeService _service;

        public CidadeController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new CidadeService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Cidade>> Incluir([FromBody]Cidade cidade)
        {
            return await _service.Adicionar(cidade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Cidade>> Put([FromBody]Cidade cidade, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(cidade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{CidadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Cidade>> Delete(string CidadeId)
        {
            return await _service.Remover(Guid.Parse(CidadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet("BuscaCidade/{NomeCidade}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Cidade>>> GetByName(string NomeCidade)
        {

            return await _service.GetByName(NomeCidade);
        }

        [HttpGet("{CidadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Cidade>> Get(string CidadeId)
        {
            return await _service.Obter(Guid.Parse(CidadeId));
        }

        //[HttpGet("GetByEstado/{estadoId}")]
        //   [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        //public async Task<CustomResponse<IList<Cidade>>> GetByEstado(string estadoId)
        //{
        //    return await _service.GetByEstado(Guid.Parse(estadoId));
        //}

        [Route("GetByEstado")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Cidade>>> GetByEstado([FromBody]Estado estado)
        {

            return await _service.GetByEstado(estado.EstadoId);
        }

    }
}

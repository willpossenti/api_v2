

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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class TipoChegadaController : Controller
    {
        private ITipoChegadaService _service;

        public TipoChegadaController(KlinikosDbContext context)
        {
            _service = new TipoChegadaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoChegada>> Incluir([FromBody]TipoChegada tipoChegada)
        {
            return await _service.Adicionar(tipoChegada, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoChegada>> Put([FromBody]TipoChegada tipoChegada, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoChegada, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{tipoChegadaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoChegada>> Delete(string tipoChegadaId)
        {
            return await _service.Remover(Guid.Parse(tipoChegadaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoChegada>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{tipoChegadaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoChegada>> Get(string tipoChegadaId)
        {
            return await _service.Obter(Guid.Parse(tipoChegadaId));
        }

      

    }
}

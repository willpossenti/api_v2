

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
    public class TipoCertidaoController : Controller
    {
        private ITipoCertidaoService _service;

        public TipoCertidaoController(KlinikosDbContext context)
        {
            _service = new TipoCertidaoService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoCertidao>> Incluir([FromBody]TipoCertidao pais)
        {
            return await _service.Adicionar(pais, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoCertidao>> Put([FromBody]TipoCertidao pais, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(pais, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TipoCertidaoId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoCertidao>> Delete(string TipoCertidaoId)
        {
            return await _service.Remover(Guid.Parse(TipoCertidaoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoCertidao>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TipoCertidaoId}")]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoCertidao>> Get(string TipoCertidaoId)
        {
            return await _service.Obter(Guid.Parse(TipoCertidaoId));
        }

      

    }
}

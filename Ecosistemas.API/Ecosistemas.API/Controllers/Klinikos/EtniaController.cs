﻿

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
    public class EtniaController : Controller
    {
        private IEtniaService _service;

        public EtniaController(KlinikosDbContext context)
        {
            _service = new EtniaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Etnia>> Incluir([FromBody]Etnia etnia)
        {
            return await _service.Adicionar(etnia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Etnia>> Put([FromBody]Etnia etnia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(etnia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EtniaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Etnia>> Delete(string EtniaId)
        {
            return await _service.Remover(Guid.Parse(EtniaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Etnia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{EtniaId}")]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Etnia>> Get(string EtniaId)
        {
            return await _service.Obter(Guid.Parse(EtniaId));
        }

      

    }
}

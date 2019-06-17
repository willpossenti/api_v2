﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize("Bearer")]
    public class GrupoExameController : Controller
    {
        private readonly IGrupoExameService _service;

        public GrupoExameController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new GrupoExameService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoExame>> Incluir([FromBody]GrupoExame grupoexame)
        {
            return await _service.Adicionar(grupoexame, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoExame>> Put([FromBody]GrupoExame grupoexame, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(grupoexame, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{GrupoExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoExame>> Delete(string GrupoExameId)
        {
            return await _service.Remover(Guid.Parse(GrupoExameId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<GrupoExame>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{GrupoExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoExame>> Get(string GrupoExameId)
        {
            return await _service.Obter(Guid.Parse(GrupoExameId));
        }



    }
}
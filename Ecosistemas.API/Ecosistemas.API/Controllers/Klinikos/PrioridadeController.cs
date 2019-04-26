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
    public class PrioridadeController : Controller
    {
        private readonly IPrioridadeService _service;

        public PrioridadeController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new PrioridadeService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Prioridade>> Incluir([FromBody]Prioridade prioridade)
        {
            return await _service.Adicionar(prioridade, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Prioridade>> Put([FromBody]Prioridade prioridade, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(prioridade, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{PrioridadeId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Prioridade>> Delete(string PrioridadeId)
        {
            return await _service.Remover(Guid.Parse(PrioridadeId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Prioridade>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{PrioridadeId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Prioridade>> Get(string PrioridadeId)
        {
            return await _service.Obter(Guid.Parse(PrioridadeId));
        }



    }
}
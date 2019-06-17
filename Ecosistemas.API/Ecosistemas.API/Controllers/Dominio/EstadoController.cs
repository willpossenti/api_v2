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
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Dominio
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class EstadoController : Controller
    {
        private readonly IEstadoService _service;

        public EstadoController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new EstadoService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Estado>> Incluir([FromBody]Estado estado)
        {
            return await _service.Adicionar(estado, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Estado>> Put([FromBody]Estado estado, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(estado, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{EstadoId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public async Task<CustomResponse<Estado>> Delete(string EstadoId)
        {
            return await _service.Remover(Guid.Parse(EstadoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Estado>>> Get()
        {
             return await _service.ListarTodos();
        }

        [HttpGet("{EstadoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Estado>> Get(string EstadoId)
        {
            return await _service.Obter(Guid.Parse(EstadoId));
        }

       
    }
}
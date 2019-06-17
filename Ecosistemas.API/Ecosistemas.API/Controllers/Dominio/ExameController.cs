using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Services.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.API.Controllers.Dominio
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ExameController : Controller
    {
        private readonly IExameService _service;

        public ExameController(DominioDbContext contextDominio, ApiDbContext context)
        {
            _service = new ExameService(contextDominio, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Exame>> Incluir([FromBody]Exame exame)
        {
            return await _service.Adicionar(exame, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Exame>> Put([FromBody]Exame exame, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(exame, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Exame>> Delete(string ExameId)
        {
            return await _service.Remover(Guid.Parse(ExameId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Exame>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ExameId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Exame>> Get(string ExameId)
        {
            return await _service.Obter(Guid.Parse(ExameId));
        }

        [HttpGet("ConsultaExame/{nome}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<Exame>>> ConsultaExame(string nome)
        {
            return await _service.ConsultaExame(nome, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

    }
}

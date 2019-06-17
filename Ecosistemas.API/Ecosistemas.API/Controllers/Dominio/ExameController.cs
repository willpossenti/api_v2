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
    [Authorize("Bearer")]
    public class ExameController : Controller
    {
        private readonly IExameService _service;

        public ExameController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ExameService(contextKlinikos, context);
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

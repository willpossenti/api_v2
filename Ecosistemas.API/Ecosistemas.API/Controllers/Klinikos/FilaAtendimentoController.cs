

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
using Ecosistemas.Business.Contexto.Dominio;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class FilaAtendimentoController : Controller
    {
        private readonly IFilaAtendimentoService _service;

        public FilaAtendimentoController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new FilaAtendimentoService(contextDominio, contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimento>> Incluir([FromBody]FilaAtendimento filaAtendimento)
        {
            return await _service.AdicionarPacienteFila(filaAtendimento, Guid.Parse("285CE313-2D96-4425-9A70-B1E71BC17020"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimento>> Put([FromBody]FilaAtendimento filaAtendimento)
        {
            return await _service.Atualizar(filaAtendimento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [Route("RetirarPacienteFila")]
        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimento>> RetirarPacienteFila([FromBody]FilaAtendimento filaAtendimento)
        {
            return await _service.RetirarPacienteFila(filaAtendimento, Guid.Parse(HttpContext.User.Identity.Name));
        }

        

        [HttpDelete("{filaAtendimentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<FilaAtendimento>> Delete(string filaAtendimentoId)
        {
            return await _service.Remover(Guid.Parse(filaAtendimentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<FilaAtendimento>>> Get()
        {
            return await _service.ConsultarFila();
        }

        //[HttpGet("{filaClassificacaoId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        //public async Task<CustomResponse<FilaClassificacao>> Get(string filaClassificacaoId)
        //{
        //    return await _service.BuscarFilaRegistroPorId(Guid.Parse(filaClassificacaoId), Guid.Parse(HttpContext.User.Identity.Name));
        //}



    }
}

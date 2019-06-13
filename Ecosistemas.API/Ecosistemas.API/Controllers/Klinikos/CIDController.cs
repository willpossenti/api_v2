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
    public class CIDController : Controller
    {
        private readonly ICIDService _service;

        public CIDController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new CIDService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CID>> Incluir([FromBody]CID cid)
        {
            return await _service.Adicionar(cid, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CID>> Put([FromBody]CID cid, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(cid, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{CIDId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CID>> Delete(string CIDId)
        {
            return await _service.Remover(Guid.Parse(CIDId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<CID>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{CIDId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<CID>> Get(string CIDId)
        {
            return await _service.Obter(Guid.Parse(CIDId));
        }

        [HttpPost("GetCIDByCapitulo")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<CID>>> GetCIDByCapitulo([FromBody]CID CID)
        {
            return await _service.GetCIDByCapitulo(CID);
        }

        [HttpGet("ConsultaCIDs/{nome}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<CID>>> ConsultaCIDs(string nome)
        {
            return await _service.ConsultaCIDs(nome, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

    }
}

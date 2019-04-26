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
    public class TraumaController : Controller
    {
        private readonly ITraumaService _service;

        public TraumaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new TraumaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Trauma>> Incluir([FromBody]Trauma trauma)
        {
            return await _service.Adicionar(trauma, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Trauma>> Put([FromBody]Trauma trauma, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(trauma, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{TraumaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Trauma>> Delete(string TraumaId)
        {
            return await _service.Remover(Guid.Parse(TraumaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Trauma>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{TraumaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Trauma>> Get(string TraumaId)
        {
            return await _service.Obter(Guid.Parse(TraumaId));
        }



    }
}

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
    public class AcolhimentoController : Controller
    {
        private readonly IAcolhimentoService _service;

        public AcolhimentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new AcolhimentoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Acolhimento>> Incluir([FromBody]Acolhimento acolhimento)
        {
            return await _service.AdicionarAcolhimento(acolhimento, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Acolhimento>> Put([FromBody]Acolhimento acolhimento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(acolhimento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{AcolhimentoId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Acolhimento>> Delete(string AcolhimentoId)
        {
            return await _service.Remover(Guid.Parse(AcolhimentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<Acolhimento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{AcolhimentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<Acolhimento>> Get(string AcolhimentoId)
        {
            return await _service.Obter(Guid.Parse(AcolhimentoId));
        }



    }
}

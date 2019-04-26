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
    public class ClassificacaoRiscoController : Controller
    {
        private readonly IClassificacaoRiscoService _service;

        public ClassificacaoRiscoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ClassificacaoRiscoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRisco>> Incluir([FromBody]ClassificacaoRisco classificacaoRisco)
        {
            return await _service.AdicionarClassificacaoRisco(classificacaoRisco, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRisco>> Put([FromBody]ClassificacaoRisco classificacaoRisco, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(classificacaoRisco, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ClassificacaoRiscoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRisco>> Delete(string ClassificacaoRiscoId)
        {
            return await _service.Remover(Guid.Parse(ClassificacaoRiscoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ClassificacaoRisco>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ClassificacaoRiscoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRisco>> Get(string ClassificacaoRiscoId)
        {
            return await _service.Obter(Guid.Parse(ClassificacaoRiscoId));
        }



    }
}

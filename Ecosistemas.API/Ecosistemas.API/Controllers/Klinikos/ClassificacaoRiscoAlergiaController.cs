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
    public class ClassificacaoRiscoAlergiaController : Controller
    {
        private readonly IClassificacaoRiscoAlergiaService _service;

        public ClassificacaoRiscoAlergiaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ClassificacaoRiscoAlergiaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRiscoAlergia>> Incluir([FromBody]ClassificacaoRiscoAlergia classificacaoRiscoAlergia)
        {
            return await _service.AdicionarClassificacaoRiscoAlergia(classificacaoRiscoAlergia, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRiscoAlergia>> Put([FromBody]ClassificacaoRiscoAlergia classificacaoRiscoAlergia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(classificacaoRiscoAlergia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ClassificacaoRiscoAlergiaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRiscoAlergia>> Delete(string ClassificacaoRiscoAlergiaId)
        {
            return await _service.Remover(Guid.Parse(ClassificacaoRiscoAlergiaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ClassificacaoRiscoAlergia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ClassificacaoRiscoAlergiaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ClassificacaoRiscoAlergia>> Get(string ClassificacaoRiscoAlergiaId)
        {
            return await _service.Obter(Guid.Parse(ClassificacaoRiscoAlergiaId));
        }



    }
}

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
    public class GrupoMedicamentoController : Controller
    {
        private readonly IGrupoMedicamentoService _service;

        public GrupoMedicamentoController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new GrupoMedicamentoService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamento>> Incluir([FromBody]GrupoMedicamento grupoMedicamento)
        {
            return await _service.AdicionarGrupoMedicamento(grupoMedicamento, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamento>> Put([FromBody]GrupoMedicamento grupoMedicamento, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(grupoMedicamento, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{GrupoMedicamentoId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamento>> Delete(string GrupoMedicamentoId)
        {
            return await _service.Remover(Guid.Parse(GrupoMedicamentoId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<GrupoMedicamento>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{GrupoMedicamentoId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamento>> Get(string GrupoMedicamentoId)
        {
            return await _service.Obter(Guid.Parse(GrupoMedicamentoId));
        }



    }
}

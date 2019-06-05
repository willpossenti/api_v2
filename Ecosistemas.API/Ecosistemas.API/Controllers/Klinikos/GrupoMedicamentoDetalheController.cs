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
    public class GrupoMedicamentoDetalheController : Controller
    {
        private readonly IGrupoMedicamentoDetalheService _service;

        public GrupoMedicamentoDetalheController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new GrupoMedicamentoDetalheService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamentoDetalhe>> Incluir([FromBody]GrupoMedicamentoDetalhe grupoMedicamentoDetalhe)
        {
            return await _service.Adicionar(grupoMedicamentoDetalhe, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamentoDetalhe>> Put([FromBody]GrupoMedicamentoDetalhe grupoMedicamentoDetalhe, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(grupoMedicamentoDetalhe, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{GrupoMedicamentoDetalheId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamentoDetalhe>> Delete(string GrupoMedicamentoDetalheId)
        {
            return await _service.Remover(Guid.Parse(GrupoMedicamentoDetalheId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<GrupoMedicamentoDetalhe>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{GrupoMedicamentoDetalheId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<GrupoMedicamentoDetalhe>> Get(string GrupoMedicamentoDetalheId)
        {
            return await _service.Obter(Guid.Parse(GrupoMedicamentoDetalheId));
        }



    }
}

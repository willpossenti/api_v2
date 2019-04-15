

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

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class TipoOcorrenciaController : Controller
    {
        private ITipoOcorrenciaService _service;

        public TipoOcorrenciaController(KlinikosDbContext context)
        {
            _service = new TipoOcorrenciaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoOcorrencia>> Incluir([FromBody]TipoOcorrencia tipoOcorrencia)
        {
            return await _service.Adicionar(tipoOcorrencia, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoOcorrencia>> Put([FromBody]TipoOcorrencia tipoOcorrencia, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(tipoOcorrencia, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{tipoOcorrenciaId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoOcorrencia>> Delete(string tipoOcorrenciaId)
        {
            return await _service.Remover(Guid.Parse(tipoOcorrenciaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<TipoOcorrencia>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{tipoOcorrenciaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<TipoOcorrencia>> Get(string tipoOcorrenciaId)
        {
            return await _service.Obter(Guid.Parse(tipoOcorrenciaId));
        }

      

    }
}

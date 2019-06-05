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
    public class ModeloPrescricaoReceitaDetalheController : Controller
    {
        private readonly IModeloPrescricaoReceitaDetalheService _service;

        public ModeloPrescricaoReceitaDetalheController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ModeloPrescricaoReceitaDetalheService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceitaDetalhe>> Incluir([FromBody]ModeloPrescricaoReceitaDetalhe modeloPrescricaoReceitaDetalhe)
        {
            return await _service.Adicionar(modeloPrescricaoReceitaDetalhe, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceitaDetalhe>> Put([FromBody]ModeloPrescricaoReceitaDetalhe modeloPrescricaoReceitaDetalhe, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(modeloPrescricaoReceitaDetalhe, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ModeloPrescricaoReceitaDetalheId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceitaDetalhe>> Delete(string ModeloPrescricaoReceitaDetalheId)
        {
            return await _service.Remover(Guid.Parse(ModeloPrescricaoReceitaDetalheId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ModeloPrescricaoReceitaDetalhe>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ModeloPrescricaoReceitaDetalheId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceitaDetalhe>> Get(string ModeloPrescricaoReceitaDetalheId)
        {
            return await _service.Obter(Guid.Parse(ModeloPrescricaoReceitaDetalheId));
        }



    }
}

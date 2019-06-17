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
    public class ModeloPrescricaoReceitaController : Controller
    {
        private readonly IModeloPrescricaoReceitaService _service;

        public ModeloPrescricaoReceitaController(KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _service = new ModeloPrescricaoReceitaService(contextKlinikos, context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceita>> Incluir([FromBody]ModeloPrescricaoReceita modeloPrescricaoReceita)
        {
            return await _service.Adicionar(modeloPrescricaoReceita, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceita>> Put([FromBody]ModeloPrescricaoReceita modeloPrescricaoReceita)
        {
            return await _service.Atualizar(modeloPrescricaoReceita, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{ModeloPrescricaoReceitaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceita>> Delete(string ModeloPrescricaoReceitaId)
        {
            return await _service.Remover(Guid.Parse(ModeloPrescricaoReceitaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<ModeloPrescricaoReceita>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{ModeloPrescricaoReceitaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<ModeloPrescricaoReceita>> Get(string ModeloPrescricaoReceitaId)
        {
            return await _service.Obter(Guid.Parse(ModeloPrescricaoReceitaId));
        }



    }
}

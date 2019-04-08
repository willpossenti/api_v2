

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
    public class LotacaoProfissionalController : Controller
    {
        private ILotacaoProfissionalService _service;

        public LotacaoProfissionalController(KlinikosDbContext context)
        {
            _service = new LotacaoProfissionalService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LotacaoProfissional>> Incluir([FromBody]LotacaoProfissional lotacaoProfissional)
        {
            return await _service.Adicionar(lotacaoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LotacaoProfissional>> Put([FromBody]LotacaoProfissional lotacaoProfissional, [FromServices]AccessManager accessManager)
        {
            return await _service.Atualizar(lotacaoProfissional, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{LotacaoProfissionalId}")]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LotacaoProfissional>> Delete(string LotacaoProfissionalId)
        {
            return await _service.Remover(Guid.Parse(LotacaoProfissionalId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        //[Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<LotacaoProfissional>>> Get()
        {
            return await _service.ListarTodos();
        }

        [HttpGet("{LotacaoProfissionalId}")]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<LotacaoProfissional>> Get(string LotacaoProfissionalId)
        {
            return await _service.Obter(Guid.Parse(LotacaoProfissionalId));
        }

        [HttpGet("ConsultaLotacoesProfissional/{PessoaId}")]
        //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<LotacaoProfissional>>> ConsultaLotacoesProfissional(string PessoaId)
        {
            return await _service.ConsultaLotacoesProfissional(Guid.Parse(PessoaId), Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }


    }
}

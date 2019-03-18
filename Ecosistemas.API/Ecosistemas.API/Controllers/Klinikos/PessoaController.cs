

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
    public class PessoaController : Controller
    {
       
        private IPessoaPacienteService _servicePaciente;
        private IPessoaContatoService _serviceContato;

        public PessoaController(KlinikosDbContext context)
        {
            _servicePaciente = new PessoaPacienteService(context);
            _serviceContato = new PessoaContatoService(context);
        }

        [Route("PessoaPaciente/Incluir")]
        [HttpPost]
      //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> IncluirPessoaPaciente([FromBody]PessoaPaciente pessoapaciente)
        {
            //return await _service.Adicionar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
            return await _servicePaciente.Adicionar(pessoapaciente, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [Route("PessoaContato/Incluir")]
        [HttpPost]
        //  [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaContato>> IncluirPessoaContato([FromBody] List<PessoaContato> pessoacontato)
        {
            //return await _service.Adicionar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
            return await _serviceContato.AdicionarRange(pessoacontato, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpPut]
     //   [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> Put([FromBody]PessoaPaciente pessoapaciente, [FromServices]AccessManager accessManager)
        {
            return await _servicePaciente.Atualizar(pessoapaciente, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{PessoaId}")]
     //   [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> Delete(string PessoaId)
        {
           // return _service.RemoverUser(Guid.Parse(UserId), Guid.Parse(HttpContext.User.Identity.Name));
            return await _servicePaciente.Remover(Guid.Parse(PessoaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
    //    [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<PessoaPaciente>>> Get()
        {
            return await _servicePaciente.ListarTodos();
        }

        [HttpGet("{PessoaId}")]
     //   [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> Get(string PessoaId)
        {
            return await _servicePaciente.Obter(Guid.Parse(PessoaId));
        }

      

    }
}

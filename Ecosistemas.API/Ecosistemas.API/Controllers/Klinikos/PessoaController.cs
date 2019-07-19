﻿

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
using Ecosistemas.Business.Contexto.Dominio;
using Microsoft.AspNetCore.Cors;

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [ApiController]
    [Authorize("Bearer")]
    public class PessoaController : Controller
    {

        private readonly IPessoaPacienteService _servicePaciente;
        private readonly IPessoaProfissionalService _serviceProfissional;

        public PessoaController(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context)
        {
            _servicePaciente = new PessoaPacienteService(contextDominio, contextKlinikos, context);
            _serviceProfissional = new PessoaProfissionalService(contextDominio, contextKlinikos, context);
        }

        [Route("PessoaPaciente/Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> IncluirPessoaPaciente([FromBody]PessoaPaciente pessoapaciente)
        {
            //return await _service.Adicionar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
            return await _servicePaciente.AdicionarPaciente(pessoapaciente, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [Route("PessoaProfissional/Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaProfissional>> IncluirPessoaProfissional([FromBody]PessoaProfissional pessoaprofissional)
        {
            //return await _service.Adicionar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
            return await _serviceProfissional.AdicionarProfissional(pessoaprofissional, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }


        [HttpPut]
        [Route("PessoaPaciente/Alterar")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> Put([FromBody]PessoaPaciente pessoapaciente)
        {
            return await _servicePaciente.AtualizarPaciente(pessoapaciente, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }


        [HttpDelete("{PessoaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> Delete(string PessoaId)
        {
           // return _service.RemoverUser(Guid.Parse(UserId), Guid.Parse(HttpContext.User.Identity.Name));
            return await _servicePaciente.Remover(Guid.Parse(PessoaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<IList<PessoaPaciente>>> Get()
        {
            return await _servicePaciente.ListarTodos();
        }

        [HttpGet("PessoaPaciente/{PessoaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> ConsultarPaciente(string PessoaId)
        {
            return await _servicePaciente.Obter(Guid.Parse(PessoaId));
        }


        [HttpGet("PessoaPaciente/ConsultaCpf/{cpf}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> ConsultaPacienteCpf(string cpf)
        {
            return await _servicePaciente.ConsultaCpf(cpf, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaCpf/{cpf}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaProfissional>> ConsultaProfissionalCpf(string cpf)
        {
            return await _serviceProfissional.ConsultaCpf(cpf, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaPaciente/ConsultaNome/{nome}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultaPacienteNome(string nome)
        {
            return await _servicePaciente.ConsultaNome(nome, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaNome/{nome}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<PessoaProfissional>>> ConsultaProfissionalNome(string nome)
        {
            return await _serviceProfissional.ConsultaNome(nome, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaPaciente/ConsultaNomeSocial/{nomesocial}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultaPacienteNomeSocial(string nomesocial)
        {
            return await _servicePaciente.ConsultaNomeSocial(nomesocial, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaNomeSocial/{nomesocial}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<PessoaProfissional>>> ConsultaProfissionalNomeSocial(string nomesocial)
        {
            return await _serviceProfissional.ConsultaNomeSocial(nomesocial, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaPaciente/ConsultaCns/{cns}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> ConsultaPacienteCns(string cns)
        {
            return await _servicePaciente.ConsultaCns(cns, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaCns/{cns}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaProfissional>> ConsultaProfissionalCns(string cns)
        {
            return await _serviceProfissional.ConsultaCns(cns, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaPaciente/ConsultaPis/{pis}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaPaciente>> ConsultaPacientePis(string pis)
        {
            return await _servicePaciente.ConsultaPis(pis, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaPis/{pis}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaProfissional>> ConsultaProfissionalPis(string pis)
        {
            return await _serviceProfissional.ConsultaPis(pis, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }


        [HttpGet("PessoaPaciente/ConsultaPacienteAcolhimento/{pesquisa}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultaPacienteAcolhimento(string pesquisa)
        {
            return await _servicePaciente.ConsultaPacienteAcolhimento(pesquisa, Guid.Parse("B9AB33C3-6697-49F4-BF30-598214D0B7F2"));
        }

        [HttpGet("PessoaProfissional/ConsultaProfissional/{UserId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public async Task<CustomResponse<PessoaProfissional>> ConsultaProfissional(string UserId)
        {
            return await _serviceProfissional.ConsultaProfissional(Guid.Parse(UserId));
        }


    }
}

﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaPacienteService : BaseService<PessoaPaciente>, IPessoaPacienteService
    {
        private readonly KlinikosDbContext _context;

        public PessoaPacienteService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<PessoaPaciente>> AdicionarPaciente(PessoaPaciente pessoapaciente, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();

            try
            {
                await base.Adicionar(pessoapaciente, userId);
                pessoapaciente.PessoaContatos = null;
                _response.Result = pessoapaciente;
                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }
    }
}
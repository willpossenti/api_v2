﻿using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class AcolhimentoService : BaseService<Acolhimento>, IAcolhimentoService
    {

        private readonly IAcolhimentoHistoricoService _serviceAcolhimentoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public AcolhimentoService(DominioDbContext dominioDbContext, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _serviceAcolhimentoHistorico = new AcolhimentoHistoricoService(dominioDbContext, contextKlinikos, context);
        }

        public async Task<CustomResponse<Acolhimento>> AdicionarAcolhimento(Acolhimento acolhimento, Guid userId)
        {
            var _response = new CustomResponse<Acolhimento>();

            try
            {
               var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();

                await this.Adicionar(acolhimento, userId);

                await _serviceAcolhimentoHistorico.AdicionarHistoricoAcolhimento(acolhimento, _pessoaMaster);


                _response.StatusCode = StatusCodes.Status201Created;
                _response.Result = acolhimento;
                _response.Message = "Incluído com sucesso";

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<IList<Acolhimento>>> ConsultaAcolhimentoPorPessoaId(Guid pessoaId, Guid userId)
        {

            var _response = new CustomResponse<IList<Acolhimento>>();


            try
            {
                var acolhimentos = await _contextKlinikos.Acolhimentos.Where(x => x.PessoaPaciente.PessoaId == pessoaId && x.Ativo).ToListAsync();
                _response.StatusCode = StatusCodes.Status200OK;
                _response.Result = acolhimentos;
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

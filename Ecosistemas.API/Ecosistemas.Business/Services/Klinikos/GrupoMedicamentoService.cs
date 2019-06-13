﻿using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class GrupoMedicamentoService : BaseService<GrupoMedicamento>, IGrupoMedicamentoService
    {

       // private IGrupoMedicamentoHistoricoService _serviceGrupoMedicamentoHistorico;
        private readonly KlinikosDbContext _contextKlinikos;

        public GrupoMedicamentoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
           // _serviceGrupoMedicamentoHistorico = new GrupoMedicamentoHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<GrupoMedicamento>> AdicionarGrupoMedicamento(GrupoMedicamento grupoMedicamento, Guid userId)
        {
            var _response = new CustomResponse<GrupoMedicamento>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();


                grupoMedicamento.Ativo = true;

                await this.Adicionar(grupoMedicamento, userId);

               // await _serviceGrupoMedicamentoHistorico.AdicionarHistoricoGrupoMedicamento(grupoMedicamento, _pessoaMaster);

                _response.StatusCode = StatusCodes.Status201Created;
                _response.Message = "Incluído com sucesso";

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
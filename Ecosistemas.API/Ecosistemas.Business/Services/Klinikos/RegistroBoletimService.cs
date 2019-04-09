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
    public class RegistroBoletimService : BaseService<RegistroBoletim>, IRegistroBoletimService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public RegistroBoletimService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(context);
        }

        public async Task<CustomResponse<RegistroBoletim>> AdicionarRegistroBoletim(RegistroBoletim registroBoletim, Guid userId)
        {
            var _response = new CustomResponse<RegistroBoletim>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();
                var numeroBoletim = _context.RegistrosBoletim.Max(x => x.NumeroBoletim);

                if (numeroBoletim != null)
                {
                    var novoCodigo = int.Parse(numeroBoletim);
                    novoCodigo++;
                    registroBoletim.NumeroBoletim = novoCodigo.ToString("000000");
                }
                else
                    registroBoletim.NumeroBoletim = "000001";

                await this.Adicionar(registroBoletim, userId);

                if (registroBoletim.Pessoa != null) 
                    await _servicePessoaHistorico.AdicionarHistoricoPaciente(registroBoletim.Pessoa, _pessoaMaster);

                

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
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaPacienteService : BaseService<PessoaPaciente>, IPessoaPacienteService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaPacienteService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(context);
        }

        public async Task<CustomResponse<PessoaPaciente>> AdicionarPaciente(PessoaPaciente pessoaPaciente, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();
      

            try
            {
                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();
                await base.Adicionar(pessoaPaciente, userId);
                await _servicePessoaHistorico.AdicionarHistoricoPaciente(pessoaPaciente, _pessoaMaster);
                pessoaPaciente.PessoaContatos = null;
                _response.Result = pessoaPaciente;
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
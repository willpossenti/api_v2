using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

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

                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.Cpf.Contains(pessoaPaciente.Cpf) || x.Cns.Contains(pessoaPaciente.Cns) || x.PisPasep.Contains(pessoaPaciente.PisPasep);
                var _cadastroEncontrado = base.ObterByExpression(_filtroNome).Result.Result.Count;

                if (_cadastroEncontrado > 0)
                {
                    _response.StatusCode = StatusCodes.Status409Conflict;
                    return _response;
                }

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


        public async Task<CustomResponse<PessoaPaciente>> ConsultaCpf(string cpf, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.Cpf.Equals(cpf);

                await Task.Run(() =>
               {

                   var _pessoaEncontrado = ObterByExpression(_filtroNome).Result.Result.FirstOrDefault();

                   if (_pessoaEncontrado != null)
                   {
                       _response.Message = "Cpf encontrado";
                       _response.StatusCode = StatusCodes.Status302Found;
                       _response.Result = _pessoaEncontrado;
                   }
                   else
                   {
                       _response.Message = "Cpf não encontrado";
                       _response.StatusCode = StatusCodes.Status404NotFound;

                   }
               });

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
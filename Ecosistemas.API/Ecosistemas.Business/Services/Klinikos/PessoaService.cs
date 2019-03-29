using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using Ecosistemas.Business.Utility;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaService : BaseService<Pessoa>,  IPessoaService
    {
        private readonly KlinikosDbContext _context;

        public PessoaService(KlinikosDbContext context): base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<Pessoa>> ConsultaCpf(string cpf, Guid userId)
        {
            var _response = new CustomResponse<Pessoa>();

            try
            {
                Expression<Func<Pessoa, bool>> _filtroNome = x => x.Cpf.Equals(cpf);

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
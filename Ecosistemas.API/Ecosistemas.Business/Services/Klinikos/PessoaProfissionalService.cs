using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using Ecosistemas.Business.Utility;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaProfissionalService : BaseService<PessoaProfissional>, IPessoaProfissionalService
    {
        private readonly KlinikosDbContext _context;

        public PessoaProfissionalService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }


        public async Task<CustomResponse<PessoaProfissional>> AdicionarProfissional(PessoaProfissional pessoaprofissional, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                await base.Adicionar(pessoaprofissional, userId);
                pessoaprofissional.PessoaContatos = null;
                pessoaprofissional.LotacoesProfissional = null;
                _response.Result = pessoaprofissional;
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
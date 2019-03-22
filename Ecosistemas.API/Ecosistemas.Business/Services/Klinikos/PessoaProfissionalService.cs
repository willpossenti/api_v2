using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using Ecosistemas.Business.Utility;
using System.Threading.Tasks;
using System.Linq;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaProfissionalService : BaseService<PessoaProfissional>, IPessoaProfissionalService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaProfissionalService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(context);
        }


        public async Task<CustomResponse<PessoaProfissional>> AdicionarProfissional(PessoaProfissional pessoaprofissional, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();
                pessoaprofissional.Master = false;

                if (!string.IsNullOrWhiteSpace(pessoaprofissional.Login))
                {
                    var login = _context.Pessoas.Max(x => x.CodigoLogin);

                    if (login != null)
                    {
                        var novoCodigo = int.Parse(login);
                        novoCodigo++;
                        pessoaprofissional.CodigoLogin = novoCodigo.ToString("000000");
                    }
                    else
                        pessoaprofissional.CodigoLogin = "000001";
                }

                await base.Adicionar(pessoaprofissional, userId);
                await _servicePessoaHistorico.AdicionarHistoricoProfissional(pessoaprofissional, _pessoaMaster);
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
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class LotacaoProfissionalService : BaseService<LotacaoProfissional>, ILotacaoProfissionalService
    {
        private readonly KlinikosDbContext _context;

        public LotacaoProfissionalService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<List<LotacaoProfissional>>> ConsultaLotacoesProfissional(Guid pessoaId, Guid userId)
        {
            var _response = new CustomResponse<List<LotacaoProfissional>>();

            try
            {
                Expression<Func<LotacaoProfissional, bool>> _filtroNome = x => x.Pessoa.PessoaId == pessoaId && x.Ativo;


                await Task.Run(() =>
                {

                    var _lotacoesEncontradas = LotacoesProfissional.Where(_filtroNome).ToList();

                    if (_lotacoesEncontradas != null)
                    {
                        var newLotacoes = new List<LotacaoProfissional>();
                        foreach (var lotacao in _lotacoesEncontradas)
                        {
                            lotacao.Pessoa = null;
                            newLotacoes.Add(lotacao);
                        }

                        _response.Message = "Lotações encontradas";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = newLotacoes;
                    }
                    else
                    {
                        _response.Message = "Lotações não encontradas";
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

        protected internal IQueryable<LotacaoProfissional> LotacoesProfissional
        {


            get
            {
                return _context.LotacoesProfissional
                    .Include(lotacao => lotacao.OrgaoEmissorProfissional)
                    .Include(lotacao => lotacao.Pessoa)
                    .Include(lotacao => lotacao.TipoProfissional);

            }
        }
    }
}
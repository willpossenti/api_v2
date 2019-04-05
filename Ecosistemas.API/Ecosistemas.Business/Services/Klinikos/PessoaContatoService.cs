using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaContatoService : BaseService<PessoaContato>, IPessoaContatoService
    {
        private readonly KlinikosDbContext _context;

        public PessoaContatoService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<List<PessoaContato>>> ConsultaContato(Guid pessoaId, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaContato>>();

            try
            {
                Expression<Func<PessoaContato, bool>> _filtroNome = x => x.Pessoa.PessoaId == pessoaId && x.Ativo;


                await Task.Run(() =>
                {

                    var _contatosEncontrados = PessoaContatos.Where(_filtroNome).ToList();

                    if (_contatosEncontrados != null)
                    {
                        var newContatos = new List<PessoaContato>();
                        foreach (var contato in _contatosEncontrados)
                        {
                            contato.Pessoa = null;
                            newContatos.Add(contato);
                        }

                        _response.Message = "Contatos encontrados";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = newContatos;
                    }
                    else
                    {
                        _response.Message = "Contatos não encontradas";
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

        protected internal IQueryable<PessoaContato> PessoaContatos
        {


            get
            {
                return _context.PessoaContatos
                    .Include(contato => contato.Pessoa);


            }
        }
    }
}
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

      

    }
}
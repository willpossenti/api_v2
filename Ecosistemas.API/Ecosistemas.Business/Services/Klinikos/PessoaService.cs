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
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaService : BaseService<Pessoa>,  IPessoaService
    {

        public PessoaService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
        }

      

    }
}
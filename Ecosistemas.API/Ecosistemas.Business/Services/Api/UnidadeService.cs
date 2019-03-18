using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Api
{

    public class UnidadeService : BaseService<Unidade>, IUnidadeService
    {
        private readonly ApiDbContext _context;

        public UnidadeService(ApiDbContext context) : base(context)
        {
            _context = context;

        }

      
    }
}

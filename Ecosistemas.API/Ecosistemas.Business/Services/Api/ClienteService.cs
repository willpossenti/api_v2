using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Api
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        private readonly ApiDbContext _context;

        public ClienteService(ApiDbContext context) : base(context)
        {
            _context = context;

        }


    }
}

using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Api
{

    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly ApiDbContext _context;

        public RoleService(ApiDbContext context) : base(context)
        {
            _context = context;

        }
    }
}

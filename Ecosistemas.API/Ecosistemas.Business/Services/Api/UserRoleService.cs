using Ecosistemas.Business.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Api
{

    public class UserRoleService: BaseService<UserRole>
    {
        private readonly ApiDbContext _context;

        public UserRoleService(ApiDbContext context)  : base (context)
        {
            _context = context;
           
        }

       

    }
}

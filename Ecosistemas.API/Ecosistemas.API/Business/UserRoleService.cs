using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecosistemas.API.Security;
using System.Security.Cryptography;

namespace Ecosistemas.API.Business
{

    public class UserRoleService: BaseService<UserRole>
    {
        private CatalogoDbContext _context;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public UserRoleService(CatalogoDbContext context)  : base (context)
        {
            _context = context;
           
        }

       

    }
}

using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.API.Business
{

    public interface IRoleService
    {

    }

    public class RoleService : BaseService<Role>, IRoleService
    {
        private CatalogoDbContext _context;

        public RoleService(CatalogoDbContext context) : base(context)
        {
            _context = context;

        }
    }
}

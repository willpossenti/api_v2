using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using Ecosistemas.API.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.API.Business
{
    public interface ILogService
    {

    }

    public class LogService : BaseService<Log>, ILogService
    {
        private CatalogoDbContext _context;

        public LogService(CatalogoDbContext context) : base(context)
        {
            _context = context;

        }

    }
}

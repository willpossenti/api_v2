
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Api
{


    public class LogService : BaseService<Log>, ILogService
    {
      
        public LogService(ApiDbContext context) : base(context)
        {

        }

    }
}

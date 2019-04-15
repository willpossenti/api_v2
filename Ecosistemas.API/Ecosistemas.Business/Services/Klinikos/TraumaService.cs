using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class TraumaService : BaseService<Trauma>, ITraumaService
    {
        private readonly KlinikosDbContext _context;

        public TraumaService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
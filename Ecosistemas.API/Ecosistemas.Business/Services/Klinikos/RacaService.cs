using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class RacaService : BaseService<Raca>, IRacaService
    {
        private readonly KlinikosDbContext _context;

        public RacaService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
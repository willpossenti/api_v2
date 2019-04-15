using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class NivelConscienciaService : BaseService<NivelConsciencia>, INivelConscienciaService
    {
        private readonly KlinikosDbContext _context;

        public NivelConscienciaService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}

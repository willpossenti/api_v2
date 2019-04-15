using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class EspecialidadeService : BaseService<Especialidade>, IEspecialidadeService
    {
        private readonly KlinikosDbContext _context;

        public EspecialidadeService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaContatoHistoricoService : BaseService<PessoaContatoHistorico>, IPessoaContatoHistoricoService
    {
        private readonly KlinikosDbContext _context;

        public PessoaContatoHistoricoService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
﻿using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class NacionalidadeService : BaseService<Nacionalidade>, INacionalidadeService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public NacionalidadeService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;
        }
    }
}
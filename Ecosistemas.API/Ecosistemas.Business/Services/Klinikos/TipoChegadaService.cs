﻿using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class TipoChegadaService : BaseService<TipoChegada>, ITipoChegadaService
    {
        private readonly KlinikosDbContext _context;

        public TipoChegadaService(KlinikosDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
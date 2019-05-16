﻿using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class ModeloPrescricaoReceitaDetalheService : BaseService<ModeloPrescricaoReceitaDetalhe>, IModeloPrescricaoReceitaDetalheService
    {

        public ModeloPrescricaoReceitaDetalheService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {

        }
    }
}
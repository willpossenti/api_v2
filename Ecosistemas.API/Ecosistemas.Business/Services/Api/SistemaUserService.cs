﻿using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Api
{

    public class SistemaUserService : BaseService<SistemaUser>, ISistemaUserService
    {

        public SistemaUserService(ApiDbContext context) : base(context)
        {

        }

      
    }
}
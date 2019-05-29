using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Services.Dominio
{
    public class TipoCertidaoService : BaseService<TipoCertidao>, ITipoCertidaoService
    {

        public TipoCertidaoService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {
        }
    }
}
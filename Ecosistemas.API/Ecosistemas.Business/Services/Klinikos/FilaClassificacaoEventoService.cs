using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;


namespace Ecosistemas.Business.Services.Klinikos
{
    public class FilaClassificacaoEventoService : BaseService<FilaClassificacaoEvento>, IFilaClassificacaoEventoService
    {

        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;

        public FilaClassificacaoEventoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
        }

       
    }
}
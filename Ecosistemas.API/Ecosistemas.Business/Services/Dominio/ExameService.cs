using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;

namespace Ecosistemas.Business.Services.Dominio
{
    public class ExameService : BaseService<Exame>, IExameService
    {

        public ExameService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {

        }

        public async Task<CustomResponse<List<Exame>>> ConsultaExame(string exame, Guid userId)
        {
            var _response = new CustomResponse<List<Exame>>();

            try
            {
                Expression<Func<Exame, bool>> _filtroExame = x => (x.Nome.StartsWith(exame) || x.Nome.Contains(exame) || x.Nome.EndsWith(exame)) && x.Ativo;



                var _listaExames = await base.ObterByExpression(_filtroExame);

                //var _listaExames = _contextKlinikos.Exames.Where(_filtroExame).Take(10).ToList();

                if (_listaExames != null)
                {

                    _response.Message = "Exame encontrado";
                    _response.StatusCode = StatusCodes.Status302Found;
                    _response.Result = _listaExames.Result.Take(10).ToList();

                }
                else
                {
                    _response.Message = "Exame não encontrado";
                    _response.StatusCode = StatusCodes.Status404NotFound;

                }



            }
            catch (Exception ex)
            {
                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);
            }

            return _response;


        }


        

    }
}
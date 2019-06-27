using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class GrupoExameDetalheService : BaseService<GrupoExameDetalhe>, IGrupoExameDetalheService
    {

        public GrupoExameDetalheService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {

        }

        public CustomResponse<GrupoExameDetalhe> AdicionarCargaGrupoExameDetalhe(List<Exame> listaExame, GrupoExame grupo, Guid userId)
        {

            var _response = new CustomResponse<GrupoExameDetalhe>();
            try
            {

                var listaNew = new List<GrupoExameDetalhe>();

                foreach (Exame item in listaExame)
                {
                    listaNew.Add(new GrupoExameDetalhe() { ExameId = item.ExameId, GrupoExame = grupo });
                }

                 base.AdicionarRange(listaNew, userId);

                _response.StatusCode = StatusCodes.Status200OK;

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
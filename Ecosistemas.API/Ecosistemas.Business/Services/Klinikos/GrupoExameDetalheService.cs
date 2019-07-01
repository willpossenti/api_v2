using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Entities.Dominio;
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

namespace Ecosistemas.Business.Services.Klinikos
{
    public class GrupoExameDetalheService : BaseService<GrupoExameDetalhe>, IGrupoExameDetalheService
    {
        private readonly KlinikosDbContext _contextKlinikos;

        public GrupoExameDetalheService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
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

                 base.AdicionarCarga(listaNew, userId);

                _response.StatusCode = StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;



        }

        public async Task<CustomResponse<IList<GrupoExameDetalhe>>> GetDetalheByGrupo(GrupoExame GrupoExame)
        {
            var _response = new CustomResponse<IList<GrupoExameDetalhe>>();

            try
            {
                await Task.Run(() =>
                {
                    Expression<Func<GrupoExameDetalhe, bool>> filtroByGrupoExame = x => x.GrupoExame.GrupoExameId == GrupoExame.GrupoExameId
                    && x.GrupoExame.Nome.Contains(GrupoExame.Nome);
                    var listaGrupoExameDetalhes = _contextKlinikos.GrupoExameDetalhes.Where(filtroByGrupoExame);

                    _response.StatusCode = StatusCodes.Status201Created;
                    _response.Message = "Incluído com sucesso";
                    _response.Result = listaGrupoExameDetalhes.ToList();

                });

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
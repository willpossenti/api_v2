using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Entities.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.Business.Services.Dominio
{
    public class CIDService : BaseService<CID>, ICIDService
    {
        private readonly DominioDbContext _contextDominio;
        private readonly ApiDbContext _context;

        public CIDService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextDominio, context)
        {
            _contextDominio = contextDominio;
        }

        public async Task<CustomResponse<IList<CID>>> GetCIDByCapitulo(CID CID)
        {
            var _response = new CustomResponse<IList<CID>>();

            try
            {
                await Task.Run(() =>
                {
                    Expression<Func<CID, bool>> filtroByCapitulo = x => x.ConsultaCID.ConsultaCIDId == CID.ConsultaCID.ConsultaCIDId
                    && x.Nome.Contains(CID.Nome);
                    var listaCids = _contextDominio.CID.Where(filtroByCapitulo);

                    _response.StatusCode = StatusCodes.Status201Created;
                    _response.Message = "Incluído com sucesso";
                    _response.Result = listaCids.Take(50).ToList();

                });

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<List<CID>>> ConsultaCIDs(string cid, Guid userId)
        {
            var _response = new CustomResponse<List<CID>>();

            try
            {
                Expression<Func<CID, bool>> _filtroCID = x => (x.Nome.StartsWith(cid) || x.Nome.Contains(cid) || x.Nome.EndsWith(cid)) && x.Ativo;



                var _listaCID = await base.ObterByExpression(_filtroCID);
                //var _listaCIDs = _contextKlinikos.CIDs.Where(_filtroCID).Take(10).ToList();

                if (_listaCID != null)
                {

                    _response.Message = "CID encontrado";
                    _response.StatusCode = StatusCodes.Status302Found;
                    _response.Result = _listaCID.Result.Take(10).ToList();

                }
                else
                {
                    _response.Message = "CID não encontrado";
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
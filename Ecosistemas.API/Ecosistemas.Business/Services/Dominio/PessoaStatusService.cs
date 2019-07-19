using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace Ecosistemas.Business.Services.Dominio
{
    public class PessoaStatusService : BaseService<PessoaStatus>, IPessoaStatusService
    {
        private readonly DominioDbContext _contextDominio;

        public PessoaStatusService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {
            _contextDominio = contextDominio;
        }

        public async Task<CustomResponse<PessoaStatus>> GetBySigla(string descricao)
        {
            var _response = new CustomResponse<PessoaStatus>();

            try
            {

                Expression<Func<PessoaStatus, bool>> _filtroDescricao = x => x.Sigla.Equals(descricao) && x.Ativo;

                await Task.Run(() =>
                {

                    _response.StatusCode = StatusCodes.Status200OK;
                    _response.Result =  _contextDominio.PessoaStatus.Where(_filtroDescricao).ToList().FirstOrDefault();
                });

            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<IList<PessoaStatus>>> GetByNomeAndArray(string[] siglas)
        {
            var _response = new CustomResponse<IList<PessoaStatus>>();

            try
            {

                Expression<Func<PessoaStatus, bool>> _filtroDescricao = x => siglas.Any(y=>y.Equals(x.Sigla)) && x.Ativo;

                await Task.Run(() =>
                {
                    _response.StatusCode = StatusCodes.Status200OK;
                    _response.Result = _contextDominio.PessoaStatus.Where(_filtroDescricao).ToList();
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
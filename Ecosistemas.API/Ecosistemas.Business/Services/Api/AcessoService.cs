using Ecosistemas.Business.Contexto.Api;
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


    public class AcessoService : BaseService<Acesso>, IAcessoService
    {
        private ApiDbContext _context;

        public AcessoService(ApiDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<Acesso>> AdicionarAcesso(Acesso acesso)
        {
            var _response = new CustomResponse<Acesso>();

            try
            {
                await _context.AddAsync<Acesso>(acesso);
                await _context.SaveChangesAsync();

                _response.StatusCode = StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
                _response.StatusCode = StatusCodes.Status417ExpectationFailed;
                Error.LogError(ex);
            }

            return _response;
        }

      
    }
}

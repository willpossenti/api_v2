using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Interfaces.Dominio;
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.Business.Services.Dominio
{
    public class MedicamentoService : BaseService<Medicamento>, IMedicamentoService
    {
        private readonly DominioDbContext _contextDominio;
        private readonly ApiDbContext _context;

        public MedicamentoService(DominioDbContext contextDominio, ApiDbContext context) : base(contextDominio, context)
        {
            _contextDominio = contextDominio;
            _context = context;
        }

        public async Task<CustomResponse<List<Medicamento>>> ConsultaMedicamento(string medicamento, Guid userId)
        {
            var _response = new CustomResponse<List<Medicamento>>();

            try
            {
                Expression<Func<Medicamento, bool>> _filtroMedicamento = x => (x.Nome.StartsWith(medicamento) || x.Nome.Contains(medicamento) || x.Nome.EndsWith(medicamento)) && x.Ativo;



                var _listaMedicamentos = await base.ObterByExpression(_filtroMedicamento);
                //var _listaMedicamentos = _contextKlinikos.Medicamentos.Where(_filtroMedicamento).Take(10).ToList();

                if (_listaMedicamentos != null)
                {

                    _response.Message = "Medicamento encontrado";
                    _response.StatusCode = StatusCodes.Status302Found;
                    _response.Result = _listaMedicamentos.Result.Take(10).ToList();

                }
                else
                {
                    _response.Message = "Medicamento não encontrado";
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
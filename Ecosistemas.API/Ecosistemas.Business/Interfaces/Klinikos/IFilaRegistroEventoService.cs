using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaRegistroEventoService : IBaseService<FilaRegistroEvento>
    {
        Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosNovos(FilaRegistroEvento filaRegistroEvento, Guid userId);
        Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosRetirados(FilaRegistroEvento filaRegistroEvento, Guid userId);
    }
}

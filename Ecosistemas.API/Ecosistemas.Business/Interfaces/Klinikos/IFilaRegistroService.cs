using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaRegistroService : IBaseService<FilaRegistro>
    {
        Task<CustomResponse<IList<FilaRegistro>>> ConsultarFila();
        Task<CustomResponse<FilaRegistro>> BuscarFilaRegistroPorId(Guid filaRegistroId, Guid userId);
        Task<CustomResponse<FilaRegistro>> AdicionarPacienteFila(FilaRegistro filaRegistro, Guid userId);
        Task<CustomResponse<FilaRegistro>> RetirarPacienteFila(FilaRegistro filaRegistro, Guid userId);

        Task<CustomResponse<FilaRegistro>> AberturaBoletim(FilaRegistro filaRegistro, Guid userId);
    }
}

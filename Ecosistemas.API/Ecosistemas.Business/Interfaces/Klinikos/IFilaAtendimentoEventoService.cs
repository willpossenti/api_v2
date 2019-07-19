using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaAtendimentoEventoService : IBaseService<FilaAtendimentoEvento>
    {
        Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosNovos(FilaAtendimentoEvento FilaAtendimentoEvento, Guid userId);
        Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosRetirados(FilaAtendimentoEvento FilaAtendimentoEvento, Guid userId);
        Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosChamadosAoPainel(FilaAtendimentoEvento FilaAtendimentoEvento, Guid userId);
        Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosCancelados(FilaAtendimentoEvento FilaAtendimentoEvento, Guid userId);
        Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosConfirmados(FilaAtendimentoEvento FilaAtendimentoEvento, Guid userId);
    }
}

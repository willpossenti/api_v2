using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaClassificacaoEventoService : IBaseService<FilaClassificacaoEvento>
    {
        Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosNovos(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId);
        Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosRetirados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId);
        Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosChamadosAoPainel(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId);
        Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosCancelados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId);
        Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosConfirmados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId);
    }
}

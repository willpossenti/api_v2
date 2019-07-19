using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaAtendimentoService : IBaseService<FilaAtendimento>
    {
        Task<CustomResponse<IList<FilaAtendimento>>> ConsultarFila();
        Task<CustomResponse<FilaAtendimento>> AdicionarPacienteFila(FilaAtendimento filaAtendimento, Guid userId);
        Task<CustomResponse<FilaAtendimento>> RetirarPacienteFila(FilaAtendimento filaAtendimento, Guid userId);
    }
}

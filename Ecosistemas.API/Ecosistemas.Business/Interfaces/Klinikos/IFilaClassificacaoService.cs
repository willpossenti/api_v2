using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IFilaClassificacaoService : IBaseService<FilaClassificacao>
    {
        Task<CustomResponse<IList<FilaClassificacao>>> ConsultarFila();
        Task<CustomResponse<FilaClassificacao>> BuscarFilaClassificacaoPorId(Guid filaClassificacaoId, Guid userId);
        Task<CustomResponse<FilaClassificacao>> AdicionarPacienteFila(FilaClassificacao filaClassificacao, Guid userId);

        Task<CustomResponse<FilaClassificacao>> RetirarPacienteFila(FilaClassificacao filaClassificacao, Guid userId);
    }
}

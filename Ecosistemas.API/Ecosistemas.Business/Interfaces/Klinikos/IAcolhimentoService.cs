using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAcolhimentoService : IBaseService<Acolhimento>
    {
        Task<CustomResponse<Acolhimento>> AdicionarAcolhimento(Acolhimento acolhimento, Guid userId);
        Task<CustomResponse<IList<Acolhimento>>> ConsultaAcolhimentoPorPessoaId(Guid pessoaId, Guid userId);
    }
}

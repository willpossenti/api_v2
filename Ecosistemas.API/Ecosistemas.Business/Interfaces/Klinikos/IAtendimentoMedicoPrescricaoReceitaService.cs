using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoPrescricaoReceitaService : IBaseService<AtendimentoMedicoPrescricaoReceita>
    {
        Task<CustomResponse<AtendimentoMedicoPrescricaoReceita>> AdicionarAtendimentoMedicoPrescricaoReceita(AtendimentoMedicoPrescricaoReceita atendimentoMedicoPrescricaoReceita, Guid userId);
    }
}

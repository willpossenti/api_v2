using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoPrescricaoReceitaDetalheService : IBaseService<AtendimentoMedicoPrescricaoReceitaDetalhe>
    {
        Task<CustomResponse<AtendimentoMedicoPrescricaoReceitaDetalhe>> AdicionarAtendimentoMedicoPrescricaoReceitaDetalhe(AtendimentoMedicoPrescricaoReceitaDetalhe atendimentoMedicoPrescricaoReceitaDetalhe, Guid userId);
    }
}

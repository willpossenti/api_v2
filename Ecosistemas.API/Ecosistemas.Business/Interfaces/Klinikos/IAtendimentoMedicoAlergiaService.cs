using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoAlergiaService : IBaseService<AtendimentoMedicoAlergia>
    {
        Task<CustomResponse<AtendimentoMedicoAlergia>> AdicionarAtendimentoMedicoAlergia(AtendimentoMedicoAlergia atendimentoMedicoAlergia, Guid userId);
    }
}

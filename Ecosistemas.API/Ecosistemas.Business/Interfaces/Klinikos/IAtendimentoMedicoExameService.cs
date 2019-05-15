using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoExameService : IBaseService<AtendimentoMedicoExame>
    {
        Task<CustomResponse<AtendimentoMedicoExame>> AdicionarAtendimentoMedicoExame(AtendimentoMedicoExame atendimentoMedicoExame, Guid userId);
    }
}

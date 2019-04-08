using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface ILotacaoProfissionalService : IBaseService<LotacaoProfissional>
    {
        Task<CustomResponse<List<LotacaoProfissional>>> ConsultaLotacoesProfissional(Guid pessoaId, Guid userId);

    }

}

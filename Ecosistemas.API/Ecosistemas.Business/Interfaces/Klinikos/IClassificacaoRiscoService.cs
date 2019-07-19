using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IClassificacaoRiscoService : IBaseService<ClassificacaoRisco>
    {
        Task<CustomResponse<ClassificacaoRisco>> AdicionarClassificacaoRisco(ClassificacaoRisco classificacaoRisco, Guid userId);
        Task<CustomResponse<IList<ClassificacaoRisco>>> ConsultaClassificacaoRiscoPorPessoaId(Guid pessoaId, Guid userId);
    }
}

using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoAlergiaHistoricoService : IBaseService<AtendimentoMedicoAlergiaHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoAlergia(AtendimentoMedicoAlergia atendimentoMedicoAlergia, PessoaProfissional pessoaProfissionalCadastro);


    }
}

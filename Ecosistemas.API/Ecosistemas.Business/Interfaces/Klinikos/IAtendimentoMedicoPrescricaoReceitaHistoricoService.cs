using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IAtendimentoMedicoPrescricaoReceitaHistoricoService : IBaseService<AtendimentoMedicoPrescricaoReceitaHistorico>
    {
        Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedicoPrescricaoReceita(AtendimentoMedicoPrescricaoReceita atendimentoMedicoPrescricaoReceita, PessoaProfissional pessoaProfissionalCadastro);


    }
}

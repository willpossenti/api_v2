using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class PessoaProfissional: Pessoa
    {

        public virtual List<LotacaoProfissional> LotacoesProfissional { get; set; }

    }
}

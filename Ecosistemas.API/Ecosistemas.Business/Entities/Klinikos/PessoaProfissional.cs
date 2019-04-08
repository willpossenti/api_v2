using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class PessoaProfissional: Pessoa
    {
        public PessoaProfissional() { this.LotacoesProfissional = new List<LotacaoProfissional>(); }

        public virtual List<LotacaoProfissional> LotacoesProfissional { get; set; }

    }
}

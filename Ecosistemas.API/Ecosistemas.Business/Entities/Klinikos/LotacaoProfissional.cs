using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class LotacaoProfissional 
    {
        [Key]
        public Guid LotacaoProfissionalId { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual TipoProfissional TipoProfissional { get; set; }

        public bool Coordenador { get; set; }

        public bool Ativo { get; set; }


    }
}

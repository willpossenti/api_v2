using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AtendimentoMedicoPrescricaoReceita
    {

        [Key]
        public Guid AtendimentoMedicoPrescricaoReceitaId { get; set; }

        public virtual AtendimentoMedico AtendimentoMedico { get; set; }

        public DateTime? DataHora { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
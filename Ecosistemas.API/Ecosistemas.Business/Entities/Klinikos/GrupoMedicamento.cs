using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class GrupoMedicamento
    {

        [Key]
        public Guid GrupoMedicamentoId { get; set; }

        public virtual GrupoMedicamentoDetalhe GrupoMedicamentoDetalhe { get; set; }

        public virtual Medicamento Medicamento { get; set; }

        public bool Ativo { get; set; }

    }
}
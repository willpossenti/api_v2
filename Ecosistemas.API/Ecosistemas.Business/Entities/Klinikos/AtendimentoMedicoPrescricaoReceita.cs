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

        public virtual GrupoMedicamentoDetalhe GrupoMedicamentoDetalhe { get; set; }

        public virtual Medicamento Medicamento { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Dose { get; set; }

        public virtual ViaAdministracaoMedicamento ViaAdministracaoMedicamento { get; set; }

        public virtual UnidadeMedicamento UnidadeMedicamento { get; set; }

        public virtual IntervaloMedicamento IntervaloMedicamento { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Observacao { get; set; }

        public bool Prescricao { get; set; }

        public bool Receita { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
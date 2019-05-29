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

        public Guid GrupoMedicamentoId { get; set; }

        public Guid MedicamentoId { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Dose { get; set; }

        public Guid ViaAdministracaoMedicamentoId { get; set; }

        public Guid UnidadeMedicamentoId { get; set; }

        public Guid IntervaloMedicamentoId { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Observacao { get; set; }

        public bool Prescricao { get; set; }

        public bool Receita { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AtendimentoMedicoPrescricaoReceitaHistorico
    {

        [Key]
        public Guid AtendimentoMedicoPrescricaoReceitaHistoricoId { get; set; }

        public virtual AtendimentoMedicoPrescricaoReceita AtendimentoMedicoPrescricaoReceita { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string GrupoMedicamento { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Medicamento { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Dose { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string ViaAdministracaoMedicamento { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string UnidadeMedicamento { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string IntervaloMedicamento { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Observacao { get; set; }

        public bool Prescricao { get; set; }

        public bool Receita { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
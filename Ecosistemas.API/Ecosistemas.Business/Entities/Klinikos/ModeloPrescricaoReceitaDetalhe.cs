using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ModeloPrescricaoReceitaDetalhe
    {

        [Key]
        public Guid ModeloPrescricaoReceitaDetalheId { get; set; }

        public virtual ModeloPrescricaoReceita ModeloPrescricaoReceita { get; set; }

        public virtual GrupoMedicamento GrupoMedicamento { get; set; }

        public virtual Medicamento Medicamento { get; set; }

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
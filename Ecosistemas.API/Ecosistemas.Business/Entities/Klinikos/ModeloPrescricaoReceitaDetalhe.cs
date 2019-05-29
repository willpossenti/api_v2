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

        public Guid GrupoMedicamento { get; set; }

        public Guid Medicamento { get; set; }

        public Guid ViaAdministracaoMedicamento { get; set; }

        public Guid UnidadeMedicamento { get; set; }

        public Guid IntervaloMedicamento { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Observacao { get; set; }

        public bool Prescricao { get; set; }

        public bool Receita { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
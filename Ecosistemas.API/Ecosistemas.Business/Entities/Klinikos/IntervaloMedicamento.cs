using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class IntervaloMedicamento
    {

        [Key]
        public Guid IntervaloMedicamentoId { get; set; }

        [Required(ErrorMessage = "A descrição do intervalo do medicamento é obrigatória")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
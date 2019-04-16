using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class EscalaDor
    {

        [Key]
        public Guid EscalaDorId { get; set; }

        [Required(ErrorMessage = "O código da escala de dor é obrigatório")]
        public int CodigoEscalaDor { get; set; }

        [Required(ErrorMessage = "O nome da escala de dor é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}   

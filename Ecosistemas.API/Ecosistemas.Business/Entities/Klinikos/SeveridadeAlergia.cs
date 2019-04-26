using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class SeveridadeAlergia
    {

        [Key]
        public Guid SeveridadeAlergiaId { get; set; }

        [Required(ErrorMessage = "O nome da severidade da alergia é obrigatório")]
        [StringLength(40, ErrorMessage = "{0} Precisa ter no máximo 40")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
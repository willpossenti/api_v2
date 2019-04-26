using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Alergia
    {

        [Key]
        public Guid AlergiaId { get; set; }

        [Required(ErrorMessage = "O nome da alergia é obrigatório")]
        [StringLength(60, ErrorMessage = "{0} Precisa ter no máximo 60")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
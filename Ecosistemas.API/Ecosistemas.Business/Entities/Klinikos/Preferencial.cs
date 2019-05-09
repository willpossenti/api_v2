using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Preferencial
    {

        [Key]
        public Guid PreferencialId { get; set; }

        [Required(ErrorMessage = "O nome do preferencial é obrigatório")]
        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AberturaOcular
    {

        [Key]
        public Guid AberturaOcularId { get; set; }

        [Required(ErrorMessage = "O nome da variavel da abertura ocular é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Variavel { get; set; }

        public int Escore { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
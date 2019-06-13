using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ConsultaCID
    {

        [Key]
        public Guid ConsultaCIDId { get; set; }

        [Required(ErrorMessage = "O capitulo do CID é obrigatório")]
        [StringLength(5, ErrorMessage = "{0} Precisa ter no máximo 5")]
        [DataType(DataType.Text)]
        public string Capitulo { get; set; }


        [Required(ErrorMessage = "O nome do CID é obrigatório")]
        [StringLength(120, ErrorMessage = "{0} Precisa ter no máximo 120")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A posicao CID é obrigatório")]
        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Posicao { get; set; }

        public int ordem { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class CausaExterna
    {

        [Key]
        public Guid CausaExternaId { get; set; }

        [Required(ErrorMessage = "A descrição da causa externa é obrigatória")]
        [StringLength(90, ErrorMessage = "{0} Precisa ter no máximo 90")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}

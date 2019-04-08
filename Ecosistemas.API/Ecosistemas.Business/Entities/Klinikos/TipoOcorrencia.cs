using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class TipoOcorrencia
    {

        [Key]
        public Guid TipoOcorrenciaId { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

    }
}

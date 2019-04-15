using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class DoencaPreExistente
    {

        [Key]
        public Guid DoencaPreExistenteId { get; set; }

        [Required(ErrorMessage = "O nome da doença pré-existente é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

    }
}

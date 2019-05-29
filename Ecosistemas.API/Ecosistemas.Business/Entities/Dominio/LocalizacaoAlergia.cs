using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class LocalizacaoAlergia
    {

        [Key]
        public Guid LocalizacaoAlergiaId { get; set; }

        [Required(ErrorMessage = "O nome da localização da alergia é obrigatório")]
        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
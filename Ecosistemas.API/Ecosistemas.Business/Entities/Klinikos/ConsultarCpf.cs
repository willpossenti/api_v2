using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    class Consultar_cpf
    {
        [Required(ErrorMessage = "O cpf é obrigatório")]
        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public string Cpf { get; set; }
    }
}

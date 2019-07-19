using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class PessoaStatus
    {

        [Key]
        public Guid PessoaStatusId { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]
        public string Sigla { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
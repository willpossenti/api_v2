using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Cidade
    {
        [Key]
        public Guid CidadeId { get; set; }

        [Required(ErrorMessage = "O nome da cidade é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public Estado Estado { get; set; } 

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; } 
    }
}
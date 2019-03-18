using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Escolaridade
    {
        [Key]
        public Guid EscolaridadeId { get; set; }

        [Required(ErrorMessage = "A descrição da escolaridade é obrigatória")]
        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}

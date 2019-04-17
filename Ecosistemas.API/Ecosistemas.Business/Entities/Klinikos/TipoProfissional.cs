using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class TipoProfissional
    {
        [Key]
        public Guid TipoProfissionalId { get; set; }

        [Required(ErrorMessage = "A descrição do tipo de profissional é obrigatória")]
        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}

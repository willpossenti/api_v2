using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Ocupacao
    {
        [Key]
        public Guid OcupacaoId { get; set; }

        [Required(ErrorMessage = "O código da ocupação é obrigatório")]
        [StringLength(6, ErrorMessage = "{0} Precisa ter no máximo 6")]
        [DataType(DataType.Text)]

        public string CodigoOcupacao { get; set; }

        [Required(ErrorMessage = "A descrição da ocupação é obrigatória")]
        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]

        public string Descricao { get; set; }

        public bool Saude { get; set; }

        public bool Regulamentado { get; set; }

        public bool Ativo { get; set; } = true;


        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; } 
    }
}
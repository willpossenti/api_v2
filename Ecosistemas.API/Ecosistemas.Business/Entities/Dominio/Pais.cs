using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Pais
    {
        [Key]
        public Guid PaisId { get; set; }

        [Required(ErrorMessage = "O código do país é obrigatório")]
        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]

        public string CodigoPais { get; set; }

        [Required(ErrorMessage = "O nome da cidade é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; } 
    }
}
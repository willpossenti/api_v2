using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Etnia
    {
        [Key]
        public Guid EtniaId { get; set; }

        [Required(ErrorMessage = "O código da etnia é obrigatório")]
        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]

        public string CodigoEtnia { get; set; }

        [Required(ErrorMessage = "O nome da etnia é obrigatório")]
        [StringLength(300, ErrorMessage = "{0} Precisa ter no máximo 300")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}

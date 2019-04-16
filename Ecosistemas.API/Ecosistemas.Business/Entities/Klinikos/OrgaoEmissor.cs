using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class OrgaoEmissor
    {
        [Key]
        public Guid OrgaoEmissorId { get; set; }

        //Campo requerido conforme certificacao isbis
        [Required(ErrorMessage = "O código do orgão é obrigatório")]
        public int CodigoOrgaoEmissor { get; set; }

        [Required(ErrorMessage = "O nome do Orgão Emissor é obrigatório")]
        [StringLength(200, ErrorMessage = "{0} Precisa ter no máximo 200")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; } 
    }
}
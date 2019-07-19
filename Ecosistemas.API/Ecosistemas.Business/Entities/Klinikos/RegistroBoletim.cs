using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class RegistroBoletim
    {

        [Key]
        public Guid RegistroBoletimId { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroBoletim { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataBoletim { get; set; }

        [Required]
        public virtual PessoaPaciente PessoaPaciente { get; set; }

 
        public virtual PessoaProfissional PessoaProfissional { get; set; }

        public Guid TipoChegadaId { get; set; }

        public Guid EspecialidadeId { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string NomeInformante { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string EnderecoInformante { get; set; }

        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public string TelefoneInformante { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string GrauParentesco { get; set; }
        public bool Ativo { get; set; } = true;



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class CID
    {

        [Key]
        public Guid CIDId { get; set; }
        
   
        public ConsultaCID ConsultaCID { get; set; }

        [StringLength(4, ErrorMessage = "{0} Precisa ter no máximo 4")]
        [DataType(DataType.Text)]
        public string Codigo { get; set; }


        [Required(ErrorMessage = "O nome do CID é obrigatório")]
        [StringLength(200, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        [DataType(DataType.Text)]
        public string Agravo { get; set; }

        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        [DataType(DataType.Text)]
        public string Sexo { get; set; }

        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        [DataType(DataType.Text)]
        public string Estadio { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
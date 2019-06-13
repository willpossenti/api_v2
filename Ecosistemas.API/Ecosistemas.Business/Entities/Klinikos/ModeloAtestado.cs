using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ModeloAtestado
    {

        [Key]
        public Guid ModeloAtestadoId { get; set; }

        [Required(ErrorMessage = "O nome do modelo do atestado é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 500")]
        [DataType(DataType.Text)]
        public string Atestado { get; set; }

        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]
        public string ValidadeAtestado { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
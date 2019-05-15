using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ModeloPrescricaoReceita
    {

        [Key]
        public Guid ModeloPrescricaoReceitaId { get; set; }

        [Required(ErrorMessage = "O nome do modelo é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public virtual PessoaProfissional PessoaProfissional { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
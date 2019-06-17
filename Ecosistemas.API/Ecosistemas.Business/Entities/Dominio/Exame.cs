using Ecosistemas.Business.Entities.Klinikos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Exame
    {

        [Key]
        public Guid ExameId { get; set; }

        public virtual GrupoExame GrupoExame { get; set; }

        [Required(ErrorMessage = "O nome da variavel da abertura ocular é obrigatório")]
        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
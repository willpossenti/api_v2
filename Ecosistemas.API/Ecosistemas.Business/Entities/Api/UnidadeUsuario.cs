using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class UnidadeUsuario
    {
        [Key]
        public Guid UnidadeUsuarioId { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public Unidade Unidade { get; set; }
    }
}

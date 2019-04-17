using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class Unidade
    {
        [Key]
        public Guid UnidadeId { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;

        public virtual Cliente Cliente { get; set; }

        public virtual List<Sistema> Sistemas { get; set; }
    }
}

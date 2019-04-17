using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class Cliente
    {

        public Cliente() { this.Unidades = new List<Unidade>(); }

        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;

        public virtual List<Unidade> Unidades { get; set; }
    }
}

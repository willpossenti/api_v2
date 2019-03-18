using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class Cliente
    {
        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public Sistema Sistemas { get; set; }

        public List<Unidade> Unidades { get; set; }
    }
}

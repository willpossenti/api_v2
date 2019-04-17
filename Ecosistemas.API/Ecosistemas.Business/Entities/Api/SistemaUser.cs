using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class SistemaUser
    {
        [Key]
        public Guid SistemaUserId { get; set; }

        [Required]
        public virtual Sistema Sistema { get; set; }
        [Required]
        public virtual User User { get; set; }

        public bool Ativo { get; set; } = true;
    }
}

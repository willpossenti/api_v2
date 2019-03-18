using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class Acesso
    {
        [Key]
        public Guid AcessoId { get; set; }

        [Required]
        public DateTime Data { get; set; }
        public string IpAcesso { get; set; }

        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.API.Model
{
    public class Log
    {
        [Key]
        public Guid LogId { get; set; }

        [Required]
        [StringLength(100)]
        public string Acao { get; set; }

        [Required]
        [StringLength(100)]
        public string LocalAcao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public User User { get; set; }
    }
}

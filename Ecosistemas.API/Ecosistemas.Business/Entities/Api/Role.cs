using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Entities.Api
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameRole { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public bool Ativo { get; set; } = true;
    }
}

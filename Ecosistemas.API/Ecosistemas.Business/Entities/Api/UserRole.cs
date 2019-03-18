using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Entities.Api
{
    public class UserRole
    {
        [Key]
        public Guid UserRoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}

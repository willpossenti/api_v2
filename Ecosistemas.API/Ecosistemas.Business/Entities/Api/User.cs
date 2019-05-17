using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Entities.Api
{
    public class User
    {
        [Key]

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O username é Obrigatório")]
        [StringLength(50, ErrorMessage = "{0} Precisa ter pelo menos 10 caracteres.", MinimumLength = 8)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é Obrigatória")]
        [StringLength(100, ErrorMessage = "Deve ser entre 5 e 100 caracteres")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "A senha precisa ter pelo menos 8 caracteres e conter 3 dos 4 passos a seguir: letras maiusculas (A-Z), letras minúsculas (a-z), números (0-9) e caracteres especiais ( !@#$%^&*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação é Obrigatória")]
        [StringLength(100, ErrorMessage = "Deve ser entre 5 e 100 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O E-mail é Obrigatório")]
        [StringLength(100, ErrorMessage = "Precisa ter no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(10, ErrorMessage = "Precisa ter no máximo 10 caracteres")]
        [DataType(DataType.Text)]
        public string Telefone { get; set; }

        public bool Master { get; set; } = false;

        public bool Ativo { get; set; } = true;

        public List<UserRole> UserRoles { get; set; }
    

        public List<SistemaUser> SistemasUser { get; set; }
    }
}

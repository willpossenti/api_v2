using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Security.Manager
{
    public class Util
    {
        public class TokenConfigurations
        {
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public int Seconds { get; set; }
            public string Teste { get; set; }
        }

        public class Token
        {
            public bool Authenticated { get; set; }
            public string Created { get; set; }
            public string Expiration { get; set; }
            public string AccessToken { get; set; }
            public string Message { get; set; }

            public string Permissao { get; set; }
    }

        public class UserMaster
        {
            public readonly string Username = "willian.possenti";
            public readonly string Password = "!q2w3e4r5t";
            public readonly string Email = "willian.possenti@ecosistemas.com.br";
        };

        public class UnidadeUsuarioMaster
        {
            public readonly string Username = "will";
            public readonly string Password = "will92";
            public readonly string Email = "willian.possenti@ecosistemas.com.br";
        };
    }
}

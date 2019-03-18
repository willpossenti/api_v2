using Ecosistemas.API.Business;
using Ecosistemas.API.Data;
using Ecosistemas.API.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Ecosistemas.API.Security.Util;

namespace Ecosistemas.API.Model
{
    public class IdentityInitializer
    {
        private readonly CatalogoDbContext _context;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private AccessManager _acessmanager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public IdentityInitializer(CatalogoDbContext context, SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations, IServiceProvider services)
        {
            _context = context;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _acessmanager = new AccessManager(context, _signingConfigurations, _tokenConfigurations, services);
        }

        public async void Initialize()
        {

            try
            {
                if (_context.Database.EnsureCreated())
                {

                    //Cria roles
                    var listaRoles = new List<Role>() {
                        new Role() {  RoleId = Guid.NewGuid() , NameRole = Roles.ROLE_API_MASTER  },
                        new Role() { RoleId = Guid.NewGuid() , NameRole = Roles.ROLE_API_USUARIOS }
                    };

                    var _userMaster = new Util.UserMaster();

                    //Cria o usuário Master
                    var _user = new User()
                    {
                        Username = _userMaster.Username,
                        Email = _userMaster.Email,
                        Password =  _userMaster.Password
                    };

                    await new UserService(_context).Incluir(_user, _acessmanager, _user.UserId);
                    await new RoleService(_context).IncluirRange(listaRoles, _user.UserId);

                    //Atribui a role master para o usuário master
                    var _role = listaRoles.Where(x => x.NameRole == Roles.ROLE_API_MASTER).FirstOrDefault();

                    var _userRole = new UserRole() { Role = _role, User = _user };

                    await new UserRoleService(_context).Incluir(_userRole, _user.UserId);

                }
            }
            catch (Exception ex)
            {

                throw new Exception(
                               ex.Message);

            }
        }
    }
}

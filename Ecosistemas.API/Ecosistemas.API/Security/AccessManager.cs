using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Ecosistemas.API.Security.Util;
using System.Text;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Ecosistemas.Business.Contexto;
using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Utility;
using Ecosistemas.API.Utility;

namespace Ecosistemas.API.Security
{
    public class AccessManager
    {
        private CatalogoDbContext _catalogoDbContext;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public IServiceProvider _services;

        public AccessManager(
          CatalogoDbContext catalogoDbContext,
          SigningConfigurations signingConfigurations,
          TokenConfigurations tokenConfigurations, IServiceProvider services)
        {
            _catalogoDbContext = catalogoDbContext;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _services = services;
        }

        public async Task<CustomResponse<User>> ValidateCredentials(User user)
        {
            var _result = new CustomResponse<User>();

            if (user != null && !String.IsNullOrWhiteSpace(user.Username))
            {
                try
                {
                    var _userFound = await _catalogoDbContext.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();

                    if (_userFound != null)
                    {

                        // Efetua o login com base no Id do usuário e sua senha

                        byte[] decodedByPassword = System.Convert.FromBase64String(_userFound.Password);

                        if (!VerifyHashedPassword(decodedByPassword, user.Password))
                        {
                            _result.Message = "Senha Incorreta";
                            _result.StatusCode = StatusCodes.Status401Unauthorized;

                        }
                        else
                        {
                            var _userRoles = _catalogoDbContext.UserRoles.Where(x => x.User.UserId == _userFound.UserId).ToList<UserRole>();

                            foreach (UserRole _userRole in _userRoles)
                            {
                                _userRole.Role = _catalogoDbContext.UserRoles.Where(x => x.UserRoleId == _userRole.UserRoleId).Select(x => x.Role).FirstOrDefault<Role>();

                            }

                            _userFound.UserRoles = _userRoles;

                            _result.Result = _userFound;
                            _result.StatusCode = StatusCodes.Status200OK;
                        }


                    }
                    else
                        _result.Message = "Usuário não encontrado";
                }
                catch (Exception ex) { _result.Message = ex.Message; }
            }




            return _result;
        }

        public Token GenerateToken(User user)
        {

            ClaimsIdentity identity = new ClaimsIdentity(
           new GenericIdentity(user.UserId.ToString(), "Login"));

            if (user.UserRoles.Any(x => x != null))
                if (user.UserRoles.Any(x => x.Role != null))
                {

                    foreach (Role role in user.UserRoles.Select(x => x.Role).ToList<Role>())
                    {

                        var _roleFound = _catalogoDbContext.Roles.Where(x => x.NameRole == role.NameRole).FirstOrDefault();
                        identity.AddClaim(new Claim(ClaimTypes.Role, _roleFound.NameRole));

                    }

                }

            DateTime _dataCriacao = DateTime.Now;
            DateTime _dataExpiracao = _dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var _handler = new JwtSecurityTokenHandler();
            var _securityToken = _handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = _dataCriacao,
                Expires = _dataExpiracao
            });
            var _token = _handler.WriteToken(_securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = _dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = _dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = _token,
                Message = "OK"
            };
        }

        public byte[] HashPassword(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // Produce a version 2 (see comment above) text hash.
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        public bool VerifyHashedPassword(byte[] hashedPassword, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // We know ahead of time the exact length of a valid hashed password payload.
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }


    }
}

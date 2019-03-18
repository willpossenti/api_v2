using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Utility;
using Ecosistemas.Security.Manager;
using static Ecosistemas.Security.Manager.Util;
using System.Security.Claims;
using System.Security.Principal;



namespace Ecosistemas.Business.Services.Api
{

    public class UserService : BaseService<User>, IUserService
    {
        private ApiDbContext _context;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private IAcessoService _acessoService;

        public UserService(ApiDbContext context) : base(context)
        {
            _context = context;
            _acessoService = new AcessoService(context);

        }


        public async Task<CustomResponse<User>> Adicionar(User user, AccessManager accessManager, Guid UserId)
        {

            var _response = new CustomResponse<User>();

            try
            {
                if (user.UserId == Guid.Empty)
                {

                    if (!_context.Users.Any(x => x.Username == user.Username || x.Email == user.Email))
                    {

                        var _rolesUser = new List<UserRole>();

                        foreach (Role role in _context.Roles.Where(x => x.NameRole != Roles.ROLE_API_MASTER).ToList<Role>())
                        {
                            _rolesUser.Add(new UserRole { Role = role, User = user });
                        }

                        var _password = Convert.ToBase64String(accessManager.HashPassword(user.Password, _rng));

                        var _userHash = new User()
                        {
                            Username = user.Username,
                            Email = user.Email,
                            Password = _password,
                            ConfirmPassword = _password,
                            UserRoles = _rolesUser
                        };

                        _response = await base.Adicionar(_userHash, UserId);
                    }
                    else
                    {

                        _response.Message = "Usuário já cadastrado";
                        _response.StatusCode = StatusCodes.Status409Conflict;
                    }

                }
                else
                {
                    user.UserId = UserId = Guid.NewGuid();
                    user.Password = user.ConfirmPassword = Convert.ToBase64String(accessManager.HashPassword(user.Password, _rng));

                    _response = await base.Adicionar(user, UserId);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<User>> Atualizar(User user, AccessManager accessManager, Guid UserId)
        {
            var _response = new CustomResponse<User>();

            try
            {
                if (user.UserId != UserId)
                {
                    var _userHash = new User()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email
                    };

                    if (!string.IsNullOrEmpty(user.Password))
                        _userHash.Password = Convert.ToBase64String(accessManager.HashPassword(user.Password, _rng));

                    _context.Update<User>(_userHash);

                    await _context.SaveChangesAsync();
                    _response.Message = "Alteração";
                    _response.StatusCode = StatusCodes.Status200OK;

                    await GerarLog(_response.Message, typeof(User).Name, UserId);
                }
                else
                {

                    _response.Message = "Não Autorizado";
                    _response.StatusCode = StatusCodes.Status401Unauthorized;

                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
          

            return _response;
        }

        public async Task<CustomResponse<IList<User>>> BuscarTodosUsers()
        {
            var _response = new CustomResponse<IList<User>>();

            try
            {
                _response.Result = await _context.Set<User>().Where(x => x.UserRoles.Any(y => y.Role.NameRole != Roles.ROLE_API_MASTER)).ToListAsync<User>();
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;

            }
         
            return _response;
        }

        public async Task<CustomResponse<User>> BuscarUser(User user)
        {
            var _response = new CustomResponse<User>();

            try
            {
                _response.Result = await _context.Set<User>().Where(x => x.UserRoles.Any(y => y.Role.NameRole != Roles.ROLE_API_MASTER) && x.UserId == user.UserId || x.Username == user.Username || x.Email == user.Email).FirstOrDefaultAsync<User>();

                _response.StatusCode = _response.Result != null? StatusCodes.Status200OK: StatusCodes.Status204NoContent;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
          

            return _response;
        }

        public async Task<CustomResponse<User>> ConfirmarSenha(User user, AccessManager accessManager, Guid UserId)
        {
            var _response = new CustomResponse<User>();

            try
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    var _userHash = new User()
                    {
                        UserId = UserId,
                        Username = user.Username,
                        Email = user.Email
                    };

                    if (!string.IsNullOrEmpty(user.Password))
                        _userHash.Password = _userHash.ConfirmPassword = Convert.ToBase64String(accessManager.HashPassword(user.Password, _rng));

                    _context.Update<User>(_userHash);

                    await _context.SaveChangesAsync();
                    _response.Message = "O Usuário alterou a senha padrão para definitiva";
                    _response.StatusCode = StatusCodes.Status200OK;

                    await GerarLog(_response.Message, typeof(User).Name, UserId);
                }
                else
                {

                    _response.Message = "Não Autorizado";
                    _response.StatusCode = StatusCodes.Status401Unauthorized;

                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                Error.LogError(ex);
            }
           


            return _response;
        }

        public async Task<CustomResponse<User>> RemoverUser(Guid Id, Guid UserId)
        {
            return await base.Remover(Id, UserId);
        }

        public async Task<CustomResponse<User>> ValidateCredentials(User user, AccessManager accessManager)
        {
            var _result = new CustomResponse<User>();

            if (user != null && !String.IsNullOrWhiteSpace(user.Username))
            {
                try
                {
                    var _userFound = await _context.Users.Where(x => x.Username == user.Username && x.Ativo).FirstOrDefaultAsync();

                    if (_userFound != null)
                    {

                        // Efetua o login com base no Id do usuário e sua senha

                        byte[] decodedByPassword = System.Convert.FromBase64String(_userFound.Password);

                        if (!accessManager.VerifyHashedPassword(decodedByPassword, user.Password))
                        {
                            _result.Message = "Senha Incorreta";
                            _result.StatusCode = StatusCodes.Status401Unauthorized;

                        }
                        else
                        {
                            var _userRoles = _context.UserRoles.Where(x => x.User.UserId == _userFound.UserId).ToList<UserRole>();

                            foreach (UserRole _userRole in _userRoles)
                            {
                                _userRole.Role = _context.UserRoles.Where(x => x.UserRoleId == _userRole.UserRoleId).Select(x => x.Role).FirstOrDefault<Role>();

                            }

                            _userFound.UserRoles = _userRoles;

                            _result.Result = _userFound;
                            _result.StatusCode = StatusCodes.Status200OK;
                        }


                    }
                    else
                        _result.Message = "Usuário não encontrado";
                }
                catch (Exception ex) { _result.Message = ex.Message; Error.LogError(ex); }

            }




            return _result;
        }

        public async Task<CustomResponse<Token>> GerarAcesso(User user, AccessManager accessManager)
        {
            var _result = new CustomResponse<Token>();

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserId.ToString(), "Login"));

            try
            {
                if (user.UserRoles.Any(x => x != null))
                    if (user.UserRoles.Any(x => x.Role != null))
                    {

                        foreach (Role role in user.UserRoles.Select(x => x.Role).ToList<Role>())
                        {
                            var _roleFound = await _context.Set<Role>().Where(x => x.NameRole == role.NameRole).FirstOrDefaultAsync<Role>();
                            identity.AddClaim(new Claim(ClaimTypes.Role, _roleFound.NameRole));

                        }

                    }

                var Token = accessManager.GenerateToken(identity);
                _result.StatusCode = StatusCodes.Status202Accepted;
                _result.Message = "Acesso Autorizado";
                _result.Result = Token;

                var acesso = new Acesso() { Data = DateTime.Now, User = user, IpAcesso = accessManager.IpAcess };

                await _acessoService.Adicionar(acesso);

            }
            catch (Exception ex)
            {

                _result.Message = ex.Message;
                _result.StatusCode = StatusCodes.Status417ExpectationFailed;
                Error.LogError(ex);
            }



            return _result;


        }

        

    }
}

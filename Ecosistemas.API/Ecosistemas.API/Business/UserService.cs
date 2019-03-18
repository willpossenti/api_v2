using Ecosistemas.API.Data;
using Ecosistemas.API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecosistemas.API.Security;
using System.Security.Cryptography;
using Ecosistemas.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.API.Business
{
    public interface IUserService
    {
        Task<CustomResponse<User>> Incluir(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<User>> Alterar(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<IList<User>>> BuscarTodosUsers();
        Task<CustomResponse<User>> BuscarUser(Guid id);
        Task<CustomResponse<User>> ConfirmarSenha(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<User>> RemoverUser(Guid id,  Guid UserId);
    }

    public class UserService : BaseService<User>, IUserService
    {
        private CatalogoDbContext _context;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public UserService(CatalogoDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<CustomResponse<User>> Incluir(User user, AccessManager accessManager, Guid UserId)
        {

            var _response = new CustomResponse<User>();

            try
            {
                if (user.UserRoles != null)
                {
                    if (user.UserRoles.Any(x => x.Role != null))
                    {
                        var _rolesUser = new List<UserRole>();

                        foreach (Role role in user.UserRoles.Where(x => x.Role.NameRole != Roles.ROLE_API_MASTER).Select(x => x.Role).ToList<Role>())
                        {
                            var _roleFound = _context.Roles.Where(x => x.NameRole == role.NameRole).FirstOrDefault();

                            _rolesUser.Add(new UserRole { Role = _roleFound, User = user });
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

                        _response = await base.Incluir(_userHash, UserId);
                    }
                }
                else
                {
                    user.UserId = UserId = Guid.NewGuid();
                    user.Password = user.ConfirmPassword = Convert.ToBase64String(accessManager.HashPassword(user.Password, _rng));

                    _response = await base.Incluir(user, UserId);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;

            }

            return _response;
        }

        public async Task<CustomResponse<User>> Alterar(User user, AccessManager accessManager, Guid UserId)
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

        public async Task<CustomResponse<User>> BuscarUser(Guid id)
        {
            var _response = new CustomResponse<User>();

            try
            {
                _response.Result = await _context.Set<User>().Where(x => x.UserRoles.Any(y => y.Role.NameRole != Roles.ROLE_API_MASTER) && x.UserId == id).FirstOrDefaultAsync<User>();

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
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
            }


            return _response;
        }

        public async Task<CustomResponse<User>> RemoverUser(Guid Id, Guid UserId)
        {
            return await base.Remover(Id, UserId);
        }
    }
}


using Ecosistemas.Business;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Services.Api;
using Ecosistemas.Business.Utility;
using Ecosistemas.Security.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcosistemasAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [EnableCors("ApiPolicy")]
    [AllowAnonymous]
    public class AuthenticateController : Controller
    {
        private readonly IUserService _service;

        public AuthenticateController(ApiDbContext context)
        {
            _service = new UserService(context);
        }

        [HttpPost]
        public async Task<CustomResponse<User>> Post(
            [FromBody]User usuario,
            [FromServices]AccessManager accessManager)
        {

            return await _service.ValidateCredentials(usuario, accessManager);

        }


        [Route("RecuperaSenha")]
        [HttpPost]
        public object RecuperaSenha([FromBody] User user)
        {
            var queryResult = _service.BuscarUser(user).Result;


            if (queryResult.StatusCode == StatusCodes.Status200OK)
            {

                return new
                {
                    Authenticated = false,
                    queryResult.Result,
                    Message = "Retornou resultados",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else {

                return new
                {
                    Authenticated = false,
                    Message = "E-mail não encontrado",
                    StatusCode = StatusCodes.Status204NoContent
                };

            }
        }

    }
}
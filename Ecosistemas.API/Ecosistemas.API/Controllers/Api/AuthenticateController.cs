
using Ecosistemas.Business;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Entities.Api;
using Ecosistemas.Business.Interfaces.Api;
using Ecosistemas.Business.Services.Api;
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
        private IUserService _service;

        public AuthenticateController(ApiDbContext context)
        {
            _service = new UserService(context);
        }

        [HttpPost]
        public object Post(
            [FromBody]User usuario,
            [FromServices]AccessManager accessManager)
        {
            var resultado = _service.ValidateCredentials(usuario, accessManager).Result;

            if (resultado.StatusCode == StatusCodes.Status200OK)
            {
                accessManager.IpAcess = HttpContext.Connection.LocalIpAddress.ToString() != "127.0.0.1" ? HttpContext.Connection.RemoteIpAddress.ToString() :
                    HttpContext.Connection.LocalIpAddress.ToString();

                return new
                {
                    Authenticated = true,
                   _service.GerarAcesso(resultado.Result, accessManager).Result,
                    resultado.Message,
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            }
            else
            {
                return new
                {
                    Authenticated = false,
                    resultado.Message,
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
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
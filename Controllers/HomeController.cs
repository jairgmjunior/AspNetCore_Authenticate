using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Shop.Services;
using Shop.Repositories;


namespace Shop.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.UserName, model.Password);
            
            if(user == null)
                return NotFound(new { message = "Usuário ou senha inválidos! "});

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new 
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionario";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}
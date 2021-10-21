using Entity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Entity.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using AutoMapper;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace ReadLater5.Controllers.API
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private ITokenService _token;
        public AuthController(ITokenService token)
        {
            _token = token;
        }
        [HttpGet]
        [Route("token")]
        public IActionResult GenerateToken([FromBody] ApplicationUserDto loginModel)
        {
            return Ok(_token.CreateToken(loginModel));
        }
    }
}

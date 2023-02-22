﻿using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand command = new RegisterCommand()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(command);
            setRefreshTokenToCookie(result.RefreshToken);
            return Created("", result);
        }
        private void setRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}

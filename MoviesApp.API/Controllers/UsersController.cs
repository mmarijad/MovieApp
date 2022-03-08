using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.API.DTOs.User;
using MoviesApp.Domain.Models;
using MoviesApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            User user = this.mapper.Map<User>(userRegistrationDto);
            var result = await this.userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //await this.userManager.AddToRoleAsync(user, "VISITOR");
            User newUser = await this.userManager.FindByNameAsync(userRegistrationDto.UserName);
            return Ok((new { Token = _userService.CreateToken(newUser) }));
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User login failed.");
            }

            User user = await this.userManager.FindByNameAsync(userLoginDto.UserName);
            return Ok((new { Token =_userService.CreateToken(user) }));
                 }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return Ok("Signed out successfully");
        }
    }
}

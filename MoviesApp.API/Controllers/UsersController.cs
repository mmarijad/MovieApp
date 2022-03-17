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
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<UsersController> _logger;
 

    public UsersController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, ILogger<UsersController> logger)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                User user = this.mapper.Map<User>(userRegistrationDto);
                var result = await this.userManager.CreateAsync(user, userRegistrationDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                User newUser = await this.userManager.FindByNameAsync(userRegistrationDto.UserName);
                return Ok((new { Token = _userService.CreateToken(newUser) }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UsersController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return BadRequest();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("User login failed.");
                }

                User user = await this.userManager.FindByNameAsync(userLoginDto.UserName);

                if (user != null && await this.userManager.CheckPasswordAsync(user, userLoginDto.Password))
                {
                    return Ok((new { Token = _userService.CreateToken(user) }));
                }
                else
                {
                    return BadRequest("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UsersController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await this.signInManager.SignOutAsync();
                return Ok("Signed out successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UsersController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }
    }
}

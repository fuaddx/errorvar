using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Pustok2.Helpers;
using Twitter.Business.Dtos.AuthsDtos;
using Twitter.Business.Dtos.EmailDtos;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Business.Helpers;
using Twitter.Core.Entities;


namespace TwitFriday.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        IEmailService _emailService;
        SignInManager<AppUser> _signInManager { get; }
        UserManager<AppUser> _userManager {  get; }
        RoleManager<IdentityRole> _roleManager { get; }

        public AuthsController(IEmailService emailService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _emailService = emailService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private async Task _sendConfirmation(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("EmailConfirmed", "Auths", new
            {
                token = token,
                username = user.UserName
            }, Request.Scheme);
            _emailService.Send(user.Email, "Confirmation Email", "Hello");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.Username
            };
            var userr = await _userManager.FindByEmailAsync(vm.Email);
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _sendConfirmation(user);
                return Ok("Email Sent!");

            }
            else
            {
                return BadRequest(result.Errors);
            } 
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser user;
            if (vm.UserNameorEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(vm.UserNameorEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UserNameorEmail);
            }
            if (user == null)
            {
                return BadRequest("Invalid username or email");
            }
            //Check Password part
            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid password");
            }
             return Ok("Login successful");

        }

        [HttpPost("Roles")]
        public async Task<bool> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                    if (!result.Succeeded)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

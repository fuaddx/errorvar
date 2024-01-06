using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Twitter.Business.Dtos.AuthsDtos;
using Twitter.Business.Dtos.EmailDtos;
using Twitter.Business.ExternalServices.Interfaces;
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

        public AuthsController(IEmailService emailService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _emailService = emailService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendConfirmationEmail(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _sendConfirmation(user);

            return Ok("Email Sent!");
        }

        private async Task _sendConfirmation(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("EmailConfirmed","Auths", new
            {
                token = token,
                username = user.UserName
            }, Request.Scheme);

            _emailService.Send(user.Email, "Confirmation Email", $"<a href='{link}'>Confirm Email</a>");
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

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
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

    }
}

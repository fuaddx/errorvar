using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthsDtos;
using Twitter.Business.Exceptions.Auth;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Business.Services.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.Services.Implements
{
    public class AuthService : IAuthService
    {
        UserManager<AppUser> _userManager { get; }
        ITokenService _tokenService { get; }
        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<TokenDto> Login(LoginDto dto)
        {
            AppUser user;
            if(dto.UserNameorEmail.Contains("@"))
            {
                user= await _userManager.FindByEmailAsync(dto.UserNameorEmail);
            }
            else
            {
                user= await _userManager.FindByNameAsync(dto.UserNameorEmail);
            }
            if (user==null)
            {
                throw new UsreNameorEmailWrongException();
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
            {
                throw new UsreNameorEmailWrongException();
            }
            return _tokenService.CreateToken(user);
        }
    }
}

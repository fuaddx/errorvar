using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthsDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> Login(LoginDto dto);
    }
}

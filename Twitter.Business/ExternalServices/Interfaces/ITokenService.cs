using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthsDtos;
using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser user);
    }
}

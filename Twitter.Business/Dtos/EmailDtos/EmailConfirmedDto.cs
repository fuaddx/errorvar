using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.EmailDtos
{
    public class EmailConfirmedDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}

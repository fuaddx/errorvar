using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Exceptions.User
{
    public class AppUserCreatedFailedException : Exception
    {
        public AppUserCreatedFailedException()
        {
        }

        public AppUserCreatedFailedException(string? message) : base(message)
        {
        }
    }
}

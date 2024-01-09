using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Exceptions.Auth
{
    internal class UsreNameorEmailWrongException : Exception
    {
        public UsreNameorEmailWrongException()
        {
        }

        public UsreNameorEmailWrongException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Exceptions.Topic
{
    public class BlogExistException : Exception
    {
        public BlogExistException():base("Topic already added")
        {
        }

        public BlogExistException(string? message) : base(message)
        {
        }
    }
}

using System.Runtime.Serialization;

namespace Twitter.Business.Exceptions.Blog
{
    internal class BlogExistException : Exception
    {
        public BlogExistException():base("Blog Already Exist")
        {
        }

        public BlogExistException(string? message) : base(message)
        {
        }

    }
}
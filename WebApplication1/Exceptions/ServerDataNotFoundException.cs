
using System;
namespace WebApplication1
{
    public class ServerDataNotFoundException : Exception
    {
        public ServerDataNotFoundException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}
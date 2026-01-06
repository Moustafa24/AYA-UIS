using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UnAuthoraizedException : Exception
    {
        public UnAuthoraizedException(string message = "Invalid Email Or Password ") : base(message)
        {

        }
    }
}

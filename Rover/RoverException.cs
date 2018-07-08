using System;
using System.Collections.Generic;
using System.Text;

namespace Rover
{
    public class RoverException : Exception
    {
        public RoverException(string message)
            : base(message)
        { }
    }
}

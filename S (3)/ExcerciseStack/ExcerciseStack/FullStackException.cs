﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseStack
{
    class FullStackException : Exception
    {
        public FullStackException(string message) : base(message)
        {
        }
    }
}

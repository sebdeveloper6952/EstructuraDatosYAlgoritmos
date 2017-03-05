using System;

namespace ExcerciseQueues
{
    class EmptyQueueException : Exception
    {
        public EmptyQueueException(string s) : base(s) { }
    }
}

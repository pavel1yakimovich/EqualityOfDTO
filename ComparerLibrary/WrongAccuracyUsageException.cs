using System;

namespace ComparerLibrary
{
    public class WrongAccuracyUsageException : Exception
    {
        public WrongAccuracyUsageException(string msg = null) : base(msg)
        {
        }
    }
}

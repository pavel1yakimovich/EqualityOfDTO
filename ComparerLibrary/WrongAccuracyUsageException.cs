using System;

namespace ComparerLibrary
{
    public class WrongAccuracyUsageException : Exception
    {
        /// <summary>
        /// Exception is thrown when AccuracyAttribute is used in a wrong way
        /// </summary>
        /// <param name="msg">message for debug</param>
        public WrongAccuracyUsageException(string msg = null) : base(msg)
        {
        }
    }
}

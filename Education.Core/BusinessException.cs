using System;

namespace Education.Core
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) {
        }
    }
}

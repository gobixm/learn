using System;

namespace Infotecs.Attika.AttikaDomain.Services.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public override string ToString()
        {
            return InnerException == null ? Message : string.Format("{0}-><{1}>", Message, InnerException);
        }
    }
}

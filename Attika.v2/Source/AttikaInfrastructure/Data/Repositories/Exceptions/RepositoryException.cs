using System;
using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return InnerException == null ? Message : string.Format("{0}-><{1}>", Message, InnerException);
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace MoviesAPI.Common
{
    [Serializable]
    public class MovieApiException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MovieApiException()
        {
        }

        public MovieApiException(string message) : base(message)
        {
        }

        public MovieApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MovieApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

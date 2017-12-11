using System;
using System.Runtime.Serialization;

namespace SwitchesAPI.Common
{
    [Serializable]
    public class SwitchApiException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SwitchApiException()
        {
        }

        public SwitchApiException(string message) : base(message)
        {
        }

        public SwitchApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SwitchApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

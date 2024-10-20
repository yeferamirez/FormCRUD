using System.Runtime.Serialization;

namespace Form.Domain.Exceptions
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
        public UserException(string message, Exception exception) : base(message, exception)
        {
        }
        protected UserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

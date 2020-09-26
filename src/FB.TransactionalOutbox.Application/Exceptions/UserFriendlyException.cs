using System;

namespace FB.TransactionalOutbox.Application.Exceptions
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public override string Message { get; }

        public UserFriendlyException(string message)
        {
            Message = message;
        }
    }
}
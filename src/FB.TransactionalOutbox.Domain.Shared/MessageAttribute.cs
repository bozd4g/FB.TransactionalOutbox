using System;
using System.Linq;

namespace FB.TransactionalOutbox.Domain.Shared
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MessageAttribute : Attribute
    {
        public string Name { get; }
        public MessageAttribute(string name)
        {
            Name = name;
        }

        public static MessageAttribute Get(Type type)
        {
            object messageAttribute = type.GetCustomAttributes(typeof(MessageAttribute), false).FirstOrDefault();
            if (messageAttribute != null && messageAttribute is MessageAttribute message)
                return message;

            return null;
        }
    }
}
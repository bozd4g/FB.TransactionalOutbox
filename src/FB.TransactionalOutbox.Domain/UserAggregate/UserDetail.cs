using System;
using System.Collections.Generic;
using FB.TransactionalOutbox.Domain.SeedWork;

namespace FB.TransactionalOutbox.Domain.UserAggregate
{
    public class UserDetail : ValueObject
    {
        public int Age { get; private set; }

        public UserDetail()
        {
        }

        public UserDetail(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age)) age--;

            Age = age;
        }

        protected override IEnumerable<object> GetValues()
        {
            yield return Age;
        }
    }
}
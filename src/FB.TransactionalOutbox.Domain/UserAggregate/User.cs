using System;
using System.ComponentModel.DataAnnotations.Schema;
using FB.TransactionalOutbox.Domain.SeedWork;
using FB.TransactionalOutbox.Domain.UserAggregate.Events;

namespace FB.TransactionalOutbox.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string TempEmail { get; private set; }
        public string Password { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string PhoneNumber { get; private set; }
        public GenderType Gender { get; private set; }

        public string About { get; private set; }
        public string PhotoUrl { get; private set; }

        public bool IsEmailConfirmed { get; private set; }
        public bool IsPhoneConfirmed { get; private set; }

        [NotMapped]
        public UserDetail Detail { get; private set; }

        public User()
        {
        }

        public User(string name, string surname, string email, string password, DateTime birthday, Guid creatorUserId = default)
        {
            Name = name;
            Surname = surname;
            Username = $"{name}.{surname}";
            Email = email;
            Password = password;
            Birthday = birthday;
            Detail = new UserDetail(birthday);
            CreatorUserId = creatorUserId;
            CreationTime = DateTime.Now;
            LastModifierUserId = creatorUserId;
            LastModificationTime = DateTime.Now;

            this.AddDomainEvent(new UserCreatedEvent(name, surname, email, password));
        }

        public void RetrieveDetails()
        {
            Detail ??= new UserDetail(this.Birthday.GetValueOrDefault());
        }

        public void ChangeEmail(string email, Guid modifierUserId = default)
        {
            TempEmail = email;
            LastModifierUserId = modifierUserId;
            LastModificationTime = DateTime.Now;
            this.AddDomainEvent(new EmailChangedEvent(email));
        }
    }
}
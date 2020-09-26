using System.Collections.Generic;

namespace FB.TransactionalOutbox.Application.Contracts
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
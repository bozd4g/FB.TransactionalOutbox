using System;
using System.Collections.Generic;
using FB.TransactionalOutbox.Application.Contracts;

namespace FB.TransactionalOutbox.Application.Dtos
{
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        public PagedResultDto(IReadOnlyList<T> items) : base(items)
        {
            TotalCount = items.Count;
        }

        public long TotalCount { get; set; }
    }
}
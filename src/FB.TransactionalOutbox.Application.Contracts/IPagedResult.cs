namespace FB.TransactionalOutbox.Application.Contracts
{
    public interface IPagedResult<T>: IListResult<T>
    {
        long TotalCount { get; set; }
    }
}
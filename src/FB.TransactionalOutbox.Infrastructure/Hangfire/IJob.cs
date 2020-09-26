using System.Threading.Tasks;

namespace FB.TransactionalOutbox.Infrastructure.Hangfire
{
    public interface IJob
    {
        Task Start();
    }
}
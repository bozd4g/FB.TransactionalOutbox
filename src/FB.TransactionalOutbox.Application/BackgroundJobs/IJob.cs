using System.Threading.Tasks;

namespace FB.TransactionalOutbox.Application.BackgroundJobs
{
    public interface IJob
    {
        Task Start();
    }
}
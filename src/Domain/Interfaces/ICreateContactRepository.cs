using NET.Mysql.Sample.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.Domain.Interfaces
{
    public interface ICreateContactRepository
    {
        Task<int> CreateContactAsync(Contact input, CancellationToken cancellation);
    }
}

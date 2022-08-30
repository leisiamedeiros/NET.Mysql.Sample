using NET.Mysql.Sample.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.Domain.Interfaces
{
    public interface IGetAllContactsRepository
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync(CancellationToken cancellation);
    }
}
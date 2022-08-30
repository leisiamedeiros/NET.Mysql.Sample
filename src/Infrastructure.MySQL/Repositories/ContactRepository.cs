using Dapper;
using MySql.Data.MySqlClient;
using NET.Mysql.Sample.Domain.Entities;
using NET.Mysql.Sample.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.Infrastructure.MySQL.Repositories
{
    public class ContactRepository : IGetAllContactsRepository, ICreateContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateContactAsync(Contact input, CancellationToken cancellation)
        {
            var command = "INSERT INTO CONTACT (CONTACT_NAME, CONTACT_EMAIL) VALUES (@ContactName, @ContactEmail);";

            using var conn = new MySqlConnection(_connectionString);

            return await conn.ExecuteAsync(command, input);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(CancellationToken cancellation)
        {
            using var conn = new MySqlConnection(_connectionString);

            var contacts = await conn.QueryAsync<Contact>(
                "SELECT CONTACT_ID as ContactId, CONTACT_NAME as ContactName, CONTACT_EMAIL as ContactEmail FROM CONTACT;"
            );

            return contacts;
        }

        public async Task<Contact> GetContactByEmail(string email, CancellationToken cancellation)
        {
            using var conn = new MySqlConnection(_connectionString);

            return await conn.QuerySingleOrDefaultAsync<Contact>(@"
                SELECT CONTACT_ID as ContactId, CONTACT_NAME as ContactName, CONTACT_EMAIL as ContactEmail FROM CONTACT WHERE CONTACT_EMAIL = @Email
            ", new { Email = email });
        }
    }
}

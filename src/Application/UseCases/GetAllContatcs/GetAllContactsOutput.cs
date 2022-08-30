using System.Collections.Generic;

namespace NET.Mysql.Sample.Application.UseCases.GetAllContatcs
{
    public class GetAllContactsOutput
    {
        public IEnumerable<ContactOutput> Contacts { get; set; }
    }

    public class ContactOutput
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

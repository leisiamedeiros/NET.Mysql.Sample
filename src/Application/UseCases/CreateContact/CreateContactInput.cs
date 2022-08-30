using MediatR;

namespace NET.Mysql.Sample.Application.UseCases.CreateContact
{
    public class CreateContactInput : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

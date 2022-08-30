using AutoMapper;
using MediatR;
using NET.Mysql.Sample.Domain.Entities;
using NET.Mysql.Sample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.Application.UseCases.CreateContact
{
    public class CreateContact : IRequestHandler<CreateContactInput>
    {
        private readonly ICreateContactRepository _createContactRepository;
        private readonly IMapper _mapper;

        public CreateContact(ICreateContactRepository createContactRepository, IMapper mapper)
        {
            _createContactRepository = createContactRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateContactInput request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);

            await _createContactRepository.CreateContactAsync(contact, cancellationToken);

            return Unit.Value;
        }
    }
}

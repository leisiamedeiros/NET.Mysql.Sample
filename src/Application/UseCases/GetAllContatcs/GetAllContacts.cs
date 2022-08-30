using AutoMapper;
using MediatR;
using NET.Mysql.Sample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.Application.UseCases.GetAllContatcs
{
    public class GetAllContacts : IRequestHandler<GetAllContactsInput, GetAllContactsOutput>
    {
        private readonly IGetAllContactsRepository _getAllContactsRepository;

        private readonly IMapper _mapper;

        public GetAllContacts(IGetAllContactsRepository getAllContactsRepository, IMapper mapper)
        {
            _getAllContactsRepository = getAllContactsRepository;
            _mapper = mapper;
        }

        public async Task<GetAllContactsOutput> Handle(GetAllContactsInput request, CancellationToken cancellationToken)
        {
            var contacts = await _getAllContactsRepository.GetAllContactsAsync(cancellationToken);

            return _mapper.Map<GetAllContactsOutput>(contacts);
        }
    }
}

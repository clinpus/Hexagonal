using Domain;

namespace Application
{
    public class CustomerHandler : ICustomerHandler
    {

        private readonly ICustomerRepository _repository; // Port du Domain

        // Injection de dépendances
        public CustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public int Create(CustomerDto clientDto)
        {
            Customer clt  = Customer.Creer(
                                        clientDto.Id,
                                        clientDto.LastName,
                                        clientDto.FirstName,
                                        clientDto.Email,
                                        clientDto.NumeroSiret
                                        );

            return _repository.Create(clt); 

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            var lstCustomer = _repository.GetAll();

            List<CustomerDto> clientDtos = new List<CustomerDto>();
            foreach (Customer client in lstCustomer)
            {
                clientDtos.Add(MapToDto(client));
            }

            return clientDtos;
        }

        public void Update(CustomerDto clientDto)
        {
            Customer clt = Customer.Creer(
                                        clientDto.Id,
                                        clientDto.LastName,
                                        clientDto.FirstName,
                                        clientDto.Email,
                                        clientDto.NumeroSiret
                                        );

            _repository.Update(clt);
        }

        CustomerDto ICustomerHandler.GetById(int id)
        {
            var client = _repository.GetById(id);
            var clientDto = MapToDto(client);
            return clientDto;
        }


        private CustomerDto MapToDto(Customer client)
        {
            var entity = new CustomerDto()
            {
                Id = client.Id,
                LastName = client.LastName,
                FirstName = client.FirstName,
                Email = client.Email,
                NumeroSiret = client.NumeroSiret
            };
            return entity;
        }
    }
}

using Domain;

namespace Application
{
    public class ClientHandler : IClientHandler
    {

        private readonly IClientRepository _repository; // Port du Domain

        // Injection de dépendances
        public ClientHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public int Create(ClientDto clientDto)
        {
            Client clt  = Client.Creer(
                                        clientDto.Id,
                                        clientDto.LastName,
                                        clientDto.FirstName,
                                        clientDto.Email,
                                        clientDto.NumeroSiret
                                        );

            return _repository.Sauvegarder(clt); 

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientDto> GetAll()
        {
            var lstClient = _repository.GetAll();

            List<ClientDto> clientDtos = new List<ClientDto>();
            foreach (Client client in lstClient)
            {
                clientDtos.Add(MapToDto(client));
            }

            return clientDtos;
        }

        public void Update(int id, ClientDto clientDto)
        {
            throw new NotImplementedException();
        }

        ClientDto IClientHandler.GetById(int id)
        {
            var client = _repository.GetById(id);
            var clientDto = MapToDto(client);
            return clientDto;
        }


        private ClientDto MapToDto(Client client)
        {
            var entity = new ClientDto()
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

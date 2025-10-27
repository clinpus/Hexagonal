

namespace Application
{
    public interface IClientHandler
    {
        // C
        int Create(ClientDto clientDto);
        // R
        ClientDto GetById(int id);
        IEnumerable<ClientDto> GetAll();
        // U
        void Update(int id, ClientDto clientDto);
        // D
        void Delete(int id);
    }
}

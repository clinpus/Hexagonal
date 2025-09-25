using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    // Projet: Application/IClientHandler.cs (Interface du port)
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

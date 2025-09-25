

namespace Domain
{
    public interface IClientRepository
    {
        // Ajout de la méthode de lecture (Port Sortant)
        Client GetById(int id);
        IEnumerable<Client> GetAll();
        int Sauvegarder(Client invoice);
    }
}

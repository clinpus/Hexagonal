

namespace Domain
{
    public interface ICustomerRepository
    {
        // Ajout de la méthode de lecture (Port Sortant)
        Customer GetById(int id);
        IEnumerable<Customer> GetAll();
        int Sauvegarder(Customer invoice);
    }
}

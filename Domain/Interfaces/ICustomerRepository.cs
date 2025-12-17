

namespace Domain
{
    public interface ICustomerRepository
    {
        // Ajout de la méthode de lecture (Port Sortant)
        Customer GetById(int id);
        IEnumerable<Customer> GetAll();
        int Create(Customer customer);
        void Update(Customer customer);
        bool Exists(int id);
    }
}



namespace Application
{
    public interface ICustomerHandler
    {
        // C
        int Create(CustomerDto clientDto);
        // R
        CustomerDto GetById(int id);
        IEnumerable<CustomerDto> GetAll();
        // U
        void Update(int id, CustomerDto clientDto);
        // D
        void Delete(int id);
    }
}

using Domain;


namespace Persistence
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Customer GetById(int id)
        {
            return MapToDomain(_context.Customers.Where(x => x.Id == id).FirstOrDefault());
        }

        public int  Sauvegarder(Customer client)
        {
            var entity = MapToEntity(client);

            _context.Customers.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public IEnumerable<Customer> GetAll()
        {
            var entitys = _context.Customers;

            if (entitys == null)
            {
                return null; // Retourne null si l'entité n'existe pas en BDD
            }

            List<Customer> lstInvocies = new List<Customer>();
            foreach (var entity in entitys)
            {
                lstInvocies.Add(MapToDomain(entity));
            }
            return lstInvocies;
        }

        private CustomerEntity MapToEntity(Customer Customer)
        {

            var entity = new CustomerEntity()
            {
                Id = Customer.Id,
                LastName = Customer.LastName,
                Email = Customer.Email,
                NumeroSiret = Customer.NumeroSiret
            };
            return entity;
        }

        private Customer MapToDomain(CustomerEntity CustomerEntity)
        {
            Customer rCustomer = Customer.Creer(
                CustomerEntity.Id,
                CustomerEntity.LastName,
                CustomerEntity.FirstName,
                CustomerEntity.Email,
                CustomerEntity.NumeroSiret
            );

             return rCustomer;
        }
    }
}

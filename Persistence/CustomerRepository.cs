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
                FirstName = Customer.FirstName,
                Email = Customer.Email,
                NumeroSiret = Customer.NumeroSiret
            };
            return entity;
        }

        private void MapToExistingEntity(Customer domain, CustomerEntity entity)
        {
            // On ne modifie jamais l'Id ici
            entity.FirstName = domain.FirstName;
            entity.LastName = domain.LastName;
            entity.Email = domain.Email;
            entity.Tel = domain.Tel;
            entity.NumeroSiret = domain.NumeroSiret;
            entity.Adresse = domain.Adresse;
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

        public int Create(Customer customer)
        {
            var entity = MapToEntity(customer);

            _context.Customers.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public void Update(Customer customer)
        {
            var existingEntity = _context.Customers.Find(customer.Id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Id} not found.");
            }

            // 2. On transfère les données du domaine vers l'entité récupérée
            MapToExistingEntity(customer, existingEntity);

            // 3. EF détecte automatiquement les changements (Dirty Checking)
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }
    }
}

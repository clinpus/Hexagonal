using Domain;


namespace Persistence
{
    public class ClientRepository : IClientRepository
    {

        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Client GetById(int id)
        {
            return MapToDomain(_context.Clients.Where(x => x.Id == id).FirstOrDefault());
        }

        public int  Sauvegarder(Client client)
        {
            var entity = MapToEntity(client);

            _context.Clients.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public IEnumerable<Client> GetAll()
        {
            var entitys = _context.Clients;

            if (entitys == null)
            {
                return null; // Retourne null si l'entité n'existe pas en BDD
            }

            List<Client> lstInvocies = new List<Client>();
            foreach (var entity in entitys)
            {
                lstInvocies.Add(MapToDomain(entity));
            }
            return lstInvocies;
        }

        private ClientEntity MapToEntity(Client Client)
        {

            var entity = new ClientEntity()
            {
                Id = Client.Id,
                LastName = Client.LastName,
                Email = Client.Email,
                NumeroSiret = Client.NumeroSiret
            };
            return entity;
        }

        private Client MapToDomain(ClientEntity ClientEntity)
        {
            Client rClient = Client.Creer(
                ClientEntity.Id,
                ClientEntity.LastName,
                ClientEntity.FirstName,
                ClientEntity.Email,
                ClientEntity.NumeroSiret
            );

             return rClient;
        }
    }
}

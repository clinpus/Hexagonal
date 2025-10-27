
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CalculTotal(int[] prices)
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            // 1. Lecture de la BDD via EF Core
            // On inclut les lignes de facture car elles sont essentielles à l'entité Facture
            var entity = _context.Invoices
                                 .Include(i => i.InvoiceLines)
                                 .FirstOrDefault(f => f.Id == id);

            if (entity == null)
            {
                return null; // Retourne null si l'entité n'existe pas en BDD
            }

            // 2. Mappage (Adaptation) : Conversion de FactureEntity en entité de Domaine (Facture)
            return MapToDomain(entity);
        }

        public IEnumerable<Invoice> GetAll()
        {
            var entitys = _context.Invoices.Include(i => i.InvoiceLines);

            if (entitys == null)
            {
                return null; // Retourne null si l'entité n'existe pas en BDD
            }

            List<Invoice> lstInvocies = new List<Invoice>();
            foreach (var entity in entitys) {
                lstInvocies.Add(MapToDomain(entity));
            }
            return lstInvocies;

        }

        public int Sauvegarder(Invoice invoice)
        {
            var entity = MapToEntity(invoice);

            _context.Invoices.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public void Update(Invoice invoice)
        {
            var entityToPersist = MapToEntity(invoice);


            // **LIGNE DE CORRECTION** : Détache l'ancienne version s'il en existe une
            var local = _context.Set<InvoiceEntity>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entityToPersist.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Invoices.Update(entityToPersist);

            _context.SaveChanges();
        }


        private InvoiceEntity MapToEntity(Invoice invoice) {

            var lstInvoiceLine = new List<InvoiceLineEntity>();

            foreach(InvoiceLine invoiceLine in invoice.InvoiceLines)
            {
                lstInvoiceLine.Add(MapToEntity(invoiceLine));
            }

            var entity = new InvoiceEntity() { 
                Id  = invoice.Id,
                ClientId = invoice.ClientId,
                Etat = invoice.Etat,
                Numero = invoice.Numero,
                DateEcheance = invoice.DateEcheance,
                DateEmission = invoice.DateEmission,
                InvoiceLines = lstInvoiceLine
            };
            return entity;
        }

        private InvoiceLineEntity MapToEntity(InvoiceLine invoiceLine) { 

            var entity = new InvoiceLineEntity() {
                Description = invoiceLine.Description,
                Quantity = invoiceLine.Quantity,
                UnitPrice = invoiceLine.UnitPrice,
                VatRate = invoiceLine.VatRate,
                VatAmount = invoiceLine.VatAmount,
                TotalTTC = invoiceLine.TotalTTC,
                TotalHT = invoiceLine.TotalHT,
                InvoiceId = invoiceLine.InvoiceId
            };
            return entity;
        }

        private Invoice MapToDomain(InvoiceEntity invoiceEntity)
        {
            var lstInvoiceLine = new List<InvoiceLine>();

            foreach (InvoiceLineEntity invoiceLineEntity in invoiceEntity.InvoiceLines)
            {
                lstInvoiceLine.Add(MapToDomain(invoiceLineEntity));
            }

            var invoice = Invoice.Reconstruire(
                                                invoiceEntity.Id,
                                                invoiceEntity.ClientId,
                                                invoiceEntity.Numero,
                                                invoiceEntity.Etat,
                                                invoiceEntity.DateEmission,
                                                invoiceEntity.DateEcheance,
                                                lstInvoiceLine
                                               );
            return invoice;
        }

        private InvoiceLine MapToDomain(InvoiceLineEntity invoiceEntity)
        {
            var invoiceLine = new InvoiceLine(
                                                invoiceEntity.Description,
                                                invoiceEntity.UnitPrice,
                                                invoiceEntity.Quantity,
                                                invoiceEntity.VatRate
                                               );
            return invoiceLine;
        }


    }
}

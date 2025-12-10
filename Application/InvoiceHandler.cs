using Domain;

namespace Application
{
    public class InvoiceHandler : IInvoiceHandler
    {
        private readonly IInvoiceRepository _repository; // Port du Domain

        public InvoiceHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public int CalculTotal(int[] prices)
        {
            return _repository.CalculTotal(prices);
        }

        // --------------------------------------
        // --------------------------------------
        public int Create(InvoiceDto dto)
        {
            return _repository.Sauvegarder(DtoToDomain(dto)); 
        }

        // --------------------------------------
        // --------------------------------------
        public InvoiceDto GetById(int id)
        {
            return MapToDto(_repository.GetById(id));
        }

        // --------------------------------------
        // --------------------------------------
        public IEnumerable<InvoiceDto> GetAll()
        {
            List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
            var  lstInvoice = _repository.GetAll();

            foreach (Invoice invoice in lstInvoice) {
                invoiceDtos.Add(MapToDto(invoice));
            }
            return invoiceDtos;
        }

        // --------------------------------------
        // --------------------------------------
        public int AddInvoiceLine(int invoiceId, InvoiceLineDto dto)
        {
            var newLine = DtoToDomain(dto);
            var invoice = _repository.GetById(invoiceId);
            if (invoice == null) throw new InvalidOperationException("Facture non trouvée.");
            invoice.AddLine(newLine);
            _repository.Update(invoice);
            return newLine.Id;
        }

        // --------------------------------------
        // --------------------------------------
        private InvoiceDto MapToDto(Invoice invoice)
        {
            List<InvoiceLineDto> invoiceLinesDto = new List<InvoiceLineDto>();
            foreach (InvoiceLine invoiceLine in invoice.InvoiceLines)
            {
                invoiceLinesDto.Add(MapToDto(invoiceLine));
            }

            var entity = new InvoiceDto()
            {
                Id          = invoice.Id,
                CustomerId    = invoice.CustomerId,
                Etat        = invoice.Etat,
                Numero      = invoice.Numero,
                DateEcheance = invoice.DateEcheance,
                DateEmission = invoice.DateEmission,
                InvoiceLines = invoiceLinesDto
            };
            return entity;
        }

        // --------------------------------------
        // --------------------------------------
        private InvoiceLineDto MapToDto(InvoiceLine invoiceLine)
        {
            var entity = new InvoiceLineDto()
            {
                Description = invoiceLine.Description,
                UnitPrice = invoiceLine.UnitPrice,
                Quantity = invoiceLine.Quantity,
                VatRate = invoiceLine.VatRate
            };
            return entity;
        }

        private Invoice DtoToDomain(InvoiceDto invoiceDto)
        {
            var lstInvoiceLine = new List<InvoiceLine>();
            foreach (InvoiceLineDto invoiceLineDto in invoiceDto.InvoiceLines)
            {
                lstInvoiceLine.Add(DtoToDomain(invoiceLineDto));
            }
            var invoice = Invoice.Reconstruire(
                                                invoiceDto.Id,
                                                invoiceDto.CustomerId,
                                                invoiceDto.Numero,
                                                invoiceDto.Etat,
                                                invoiceDto.DateEmission,
                                                invoiceDto.DateEcheance,
                                                lstInvoiceLine
                                               );
            return invoice;
        }

        private InvoiceLine DtoToDomain(InvoiceLineDto invoiceLineDto)
        {
            var invoiceLine = new InvoiceLine(
                                                invoiceLineDto.Description,
                                                invoiceLineDto.UnitPrice,
                                                invoiceLineDto.Quantity,
                                                invoiceLineDto.VatRate
                                               );
            return invoiceLine;
        }

    }
}

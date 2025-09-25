using Domain;


namespace Application
{
    public interface IInvoiceHandler
    {
        int Create(InvoiceDto dto);
        int AddInvoiceLine(int invoiceId, InvoiceLineDto dto);
        InvoiceDto GetById(int id);
        IEnumerable<InvoiceDto> GetAll();
        int CalculTotal(int[] prices);
    }
}


namespace Application
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }  
        public decimal VatRate { get; set; }    
    }
}

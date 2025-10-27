namespace Persistence
{
    public class InvoiceLineEntity
    {
        public int InvoiceId { get; set; }
        public int Id { get; set; }

        // Propriétés de base
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal VatRate { get; set; }


        public decimal VatAmount { get; set; }
        public decimal TotalTTC { get; set; }
        public decimal TotalHT { get; set; }
        public InvoiceEntity Invoice { get; set; }

    }
}

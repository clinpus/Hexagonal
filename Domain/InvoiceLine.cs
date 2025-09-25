

namespace Domain
{
    public class InvoiceLine
    {
        public int Id { get; }
        public int InvoiceId { get; }
        public string Description { get; }
        public decimal UnitPrice { get; } 
        public int Quantity { get; }
        public decimal VatRate { get; }  

        // Propriétés calculées
        public decimal TotalHT => UnitPrice * Quantity;
        public decimal VatAmount => TotalHT * (VatRate / 100); // Montant TVA
        public decimal TotalTTC => TotalHT + VatAmount;

        // **********************************
        // 1. CONSTRUCTEUR
        // **********************************
        public InvoiceLine(string description, decimal unitPrice, int quantity, decimal vatRate)
        {
            // Validation des invariants à la création
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be positive.", nameof(unitPrice));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));
            if (vatRate < 0)
                throw new ArgumentException("VAT rate cannot be negative.", nameof(vatRate));

            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
            VatRate = vatRate;
        }

        // **********************************
        // 2. ÉGALITÉ BASÉE SUR LA VALEUR
        // Assure que deux lignes sont considérées égales si toutes leurs propriétés sont identiques.
        // **********************************
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            var other = (InvoiceLine)obj;

            return Description == other.Description &&
                   UnitPrice == other.UnitPrice &&
                   Quantity == other.Quantity &&
                   VatRate == other.VatRate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description, UnitPrice, Quantity, VatRate);
        }
    }
}

namespace Persistence.Entity
{
    public class InvoiceLineEntity
    {
        // Clé Étrangère : Lien vers l'entité Invoice
        // EF Core utilise ceci pour lier la ligne à la facture
        public int InvoiceId { get; set; }

        // Clé Composition/Ordre : Utilisé pour identifier la ligne dans le contexte d'une Invoice
        // Souvent appelée 'LineNumber' ou 'Id' dans ce contexte (peut être une clé primaire composite)
        public int Id { get; set; }

        // Propriétés de base
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal VatRate { get; set; }


        public decimal VatAmount { get; set; }
        public decimal TotalTTC { get; set; }
        public decimal TotalHT { get; set; }


        // Propriété de navigation (facultatif, pour remonter vers l'Invoice)
        public InvoiceEntity Invoice { get; set; }

        // NOTE : Pas de logique métier (pas de calculs TotalHT/TTC ici)
    }
}

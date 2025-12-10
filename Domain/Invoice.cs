using System.Collections.Generic;

namespace Domain
{
    public class Invoice
    {
        public int Id { get; set; } 
        public int CustomerId { get;  set; }
        public Customer Customer { get;  set; }

        // *** Données de Facture ***
        public string Numero { get; private set; }
        public DateTime DateEmission { get; private set; }
        public DateTime DateEcheance { get; private set; }

        // *** Collection de Lignes (Value Objects) ***
        private readonly List<InvoiceLine> _invoiceLines = new();
        public IReadOnlyList<InvoiceLine> InvoiceLines => _invoiceLines.AsReadOnly();

        // *** Invariant/État ***
        public string Etat { get; private set; } = "Brouillon";

        // *** Propriétés Calculées (Dépendent des InvoiceLines) ***
        public decimal TotalHT => _invoiceLines.Sum(line => line.TotalHT);
        public decimal TotalTTC => _invoiceLines.Sum(line => line.TotalTTC);

        internal Invoice(   
                            int     Id, 
                            int     CustomerId, 
                            string  Numero,
                            string  etatLu,
                            DateTime DateEmission,
                            DateTime DateEcheance,
                            List<InvoiceLine> InvoiceLinesList)
        {
            this.Id = Id;
            this.CustomerId = CustomerId;
            this.Numero = Numero;
            this.Etat = etatLu;
            this.DateEmission = DateEmission;
            this.DateEcheance = DateEcheance;
            this._invoiceLines = InvoiceLinesList;
        }

        // -----------------------------------------------------------------
        // FABRIQUE STATIQUE : Pour la *reconstruction* depuis la BDD
        // -----------------------------------------------------------------
        public static Invoice Reconstruire(
            int Id,
            int CustomerId,
            string Numero,
            string Etat,
            DateTime DateEmission,
            DateTime DateEcheance,
            List<InvoiceLine> InvoiceLinesList
        )
        {
            var invoice = new Invoice( Id, CustomerId, Numero, Etat, DateEmission, DateEcheance, InvoiceLinesList);
            return invoice;
        }

        private int CalculTotal(int[] prices)
        {
            int ret = 0;
            foreach (int p in prices)
            {
                ret += p;
            }
            return ret;
        }

        public void AddLine(InvoiceLine invoiceline)
        {
            _invoiceLines.Add(invoiceline);
        }

    }
}

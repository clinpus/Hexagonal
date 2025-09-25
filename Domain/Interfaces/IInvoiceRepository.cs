namespace Domain
{
    public interface IInvoiceRepository
    {
        // Ajout de la méthode de lecture (Port Sortant)
        Invoice GetById(int id);
        IEnumerable<Invoice> GetAll();
        int Sauvegarder(Invoice invoice);

        // Le Repository doit permettre de mettre à jour l'agrégat complet
        void Update(Invoice invoice);

        int CalculTotal(int[] prices);

    }
}

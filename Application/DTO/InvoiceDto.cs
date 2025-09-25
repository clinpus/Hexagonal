
using System.ComponentModel.DataAnnotations;


namespace Application
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le numero de facture est obligatoire.")]
        public string Numero { get; set; }
        public DateTime DateEmission { get; set; }
        public DateTime DateEcheance { get; set; }
        public string Etat { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<InvoiceLineDto> InvoiceLines { get; set; }
    }
}

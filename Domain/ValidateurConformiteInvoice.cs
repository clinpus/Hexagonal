using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class ValidateurConformiteInvoice : IValidateurConformiteInvoice
    {
        public List<string> Valider(Invoice invoice)
        {
            var erreurs = new List<string>();

            // Logique complexe du Domaine, qui n'est pas simple sur l'entité :

            // Règle 1 : Numérotation séquentielle stricte
            if (string.IsNullOrEmpty(invoice.Numero) || !Regex.IsMatch(invoice.Numero, @"^FR-\d{6}$"))
            {
                erreurs.Add("La numérotation de la invoice ne respecte pas le format légal 'FR-XXXXXX'.");
            }

            // Règle 2 : Vérification du client (si client pro)
            if (invoice.Customer.EstProfessionnel && string.IsNullOrEmpty(invoice.Customer.NumeroSiret))
            {
                erreurs.Add("Un client professionnel doit avoir un numéro SIRET valide.");
            }

            // Règle 3 : Taux de TVA obligatoire si total > 0
            if (invoice.TotalTTC > 0 && !invoice.InvoiceLines.Any(l => l.VatRate > 0))
            {
                erreurs.Add("Une invoice payante doit contenir des lignes avec un taux de TVA appliqué.");
            }

            // ... (etc., pour la date d'émission, les mentions légales de pénalité, etc.)

            return erreurs;
        }
    }
}

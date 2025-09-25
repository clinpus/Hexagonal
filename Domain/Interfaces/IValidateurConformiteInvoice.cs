using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IValidateurConformiteInvoice
    {

        // Méthode qui renvoie une liste d'erreurs/warnings de conformité
        List<string> Valider(Invoice invoice);
    }
}

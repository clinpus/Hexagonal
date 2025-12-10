using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ProductDto
    {
        [Required] // Validation à l'arrivée
        public string Name { get; set; }
        public decimal Price { get; set; }
        // L'Id est souvent omis ou facultatif.
    }
}

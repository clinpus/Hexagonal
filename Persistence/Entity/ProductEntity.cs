using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entity
{
    public class ProductEntity
    {
        [Key] // Clé primaire
        public int Id { get; set; }
        [Required] // Non nul en BDD
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    }
}

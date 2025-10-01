﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }  
        public decimal VatRate { get; set; }    
    }
}

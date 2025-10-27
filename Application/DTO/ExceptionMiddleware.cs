using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "Une erreur est survenue.";
        public string? Details { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

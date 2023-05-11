using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Responses
{
    public class ErrorResponse
    {
        public List<string> Messages { get; set; } = new();
        public string? Source { get; set; }
        public string? Exception { get; set; }
        public string? ErrorId { get; set; }
        public int StatusCode { get; set; }
    }
}

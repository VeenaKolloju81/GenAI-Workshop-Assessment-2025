using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenAI_Workshop_Assessment_2025.DTOs
{
    public class RequestResponse
    {
                public string TransactionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
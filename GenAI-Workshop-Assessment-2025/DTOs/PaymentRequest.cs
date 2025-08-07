using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenAI_Workshop_Assessment_2025.DTOs
{
    public class PaymentRequest
    {
            public string CustomerId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; }   = string.Empty;  
    }
}
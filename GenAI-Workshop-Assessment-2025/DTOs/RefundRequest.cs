using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenAI_Workshop_Assessment_2025.DTOs
{
    public class RefundRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        
    }
}
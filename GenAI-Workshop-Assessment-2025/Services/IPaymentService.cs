using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using GenAI_Workshop_Assessment_2025.DTOs;
using GenAI_Workshop_Assessment_2025.Services;

namespace GenAI_Workshop_Assessment_2025.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);
        Task<RefundResponse> ProcessRefundAsync(RefundRequest request);
        Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionId);
    }
}
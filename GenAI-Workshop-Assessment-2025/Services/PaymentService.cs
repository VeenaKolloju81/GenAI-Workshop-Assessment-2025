using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GenAI_Workshop_Assessment_2025.DTOs;
using Microsoft.Extensions.Logging;
using GenAI_Workshop_Assessment_2025.Services;

namespace GenAI_Workshop_Assessment_2025.Services
{
    public class PaymentService : IPaymentService  // Note: Check interface name consistency here
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(HttpClient httpClient, ILogger<PaymentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/payments", request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
                if (result == null)
                {
                    _logger.LogError("Payment API returned no data.");
                    return new PaymentResponse { Status = "Failed", Message = "No data received" };
                }
                _logger.LogInformation("Payment processed successfully: {TransactionId}", result.TransactionId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment");
                return new PaymentResponse { Status = "Failed", Message = ex.Message };
            }
        }

        public async Task<RefundResponse> ProcessRefundAsync(RefundRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/refunds", request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<RefundResponse>();
                if (result == null)
                {
                    _logger.LogError("Refund API returned no data.");
                    return new RefundResponse { Status = "Failed", Message = "No data received" };
                }
                _logger.LogInformation("Refund processed successfully: {RefundId}", result.RefundId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing refund");
                return new RefundResponse { Status = "Failed", Message = ex.Message };
            }
        }

        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PaymentStatusResponse>($"/api/payments/status/{transactionId}");
                if (response == null)
                {
                    _logger.LogError("Payment status API returned no data.");
                    return new PaymentStatusResponse { Status = "Unknown", Message = "No data received" };
                }
                _logger.LogInformation("Payment status fetched for transaction: {TransactionId}", transactionId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payment status");
                return new PaymentStatusResponse { Status = "Unknown", Message = ex.Message };
            }
        }
    }
}

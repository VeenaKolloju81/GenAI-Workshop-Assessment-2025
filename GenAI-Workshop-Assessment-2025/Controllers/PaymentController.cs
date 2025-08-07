using System.Threading.Tasks;
using GenAI_Workshop_Assessment_2025.DTOs;
using GenAI_Workshop_Assessment_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAI_Workshop_Assessment_2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var result = await _paymentService.ProcessPaymentAsync(request);
            return Ok(result);
        }

        [HttpPost("refund")]
        public async Task<IActionResult> ProcessRefund([FromBody] RefundRequest request)
        {
            var result = await _paymentService.ProcessRefundAsync(request);
            return Ok(result);
        }

        [HttpGet("status/{transactionId}")]
        public async Task<IActionResult> GetPaymentStatus(string transactionId)
        {
            var result = await _paymentService.GetPaymentStatusAsync(transactionId);
            return Ok(result);
        }
    }
}

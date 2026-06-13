using DevFreela.Core.DTOs.Request;
using DevFreela.Core.Interfaces;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> ProcessPayment(PaymentInfoRequest paymentDto)
        {
            // TODO: implementar lógica de pagamento
            return await Task.FromResult(true);
        }
    }
}

using DevFreela.Core.DTOs.Request;

namespace DevFreela.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoRequest paymentDto);
    }
}

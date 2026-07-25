using CursosApp.Dtos.Payments;

namespace CursosApp.Services.Payments
{

    public class SandboxPaymentGatewayService : IPaymentGatewayService
    {

        private const string CARD_APPROVED = "4242424242424242";
        private const string CARD_DECLINED = "4000000000000002";
        private const string CARD_EXPIRED = "4000000000000069";

        public async Task<PaymentResultDto> ProcessPaymentAsync(PaymentRequestDto request)
        {

            await Task.Delay(1200);

            var card = (request.CardNumber ?? "").Replace(" ", "").Trim();

            var reference = "sandbox_" + Guid.NewGuid().ToString("N").Substring(0, 16);

            if (card == CARD_DECLINED)
            {
                return new PaymentResultDto
                {
                    Approved = false,
                    Reference = reference,
                    Message = "Pago rechazado: la tarjeta fue declinada por el emisor."
                };
            }

            if (card == CARD_EXPIRED)
            {
                return new PaymentResultDto
                {
                    Approved = false,
                    Reference = reference,
                    Message = "Pago rechazado: la tarjeta está expirada."
                };
            }

            if (card == CARD_APPROVED)
            {
                return new PaymentResultDto
                {
                    Approved = true,
                    Reference = reference,
                    Message = "Pago aprobado en ambiente de pruebas (sandbox)."
                };
            }

            var random = new Random();
            if (random.Next(0, 100) < 10) 
            {
                return new PaymentResultDto
                {
                    Approved = false,
                    Reference = reference,
                    Message = "Pago rechazado: error temporal de la pasarela, intenta de nuevo."
                };
            }

            return new PaymentResultDto
            {
                Approved = true,
                Reference = reference,
                Message = "Pago aprobado en ambiente de pruebas (sandbox)."
            };
        }
    }
}
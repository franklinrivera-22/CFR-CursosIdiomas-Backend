namespace CursosApp.Dtos.Payments
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string OrderId { get; set; }        
        public string CustomerEmail { get; set; }
    }
}
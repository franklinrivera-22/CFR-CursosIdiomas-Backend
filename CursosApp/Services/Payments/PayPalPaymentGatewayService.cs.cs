using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CursosApp.Dtos.Payments;

namespace CursosApp.Services.Payments
{
    public class PayPalPaymentGatewayService : IPaymentGatewayService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public PayPalPaymentGatewayService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        private string BaseUrl => _config["PayPal:BaseUrl"];
        private string Currency => _config["PayPal:Currency"] ?? "USD";

  
        private async Task<string> GetAccessTokenAsync()
        {
            var creds = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{_config["PayPal:ClientId"]}:{_config["PayPal:ClientSecret"]}"));

            var req = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/v1/oauth2/token");
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", creds);
            req.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();
            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            return doc.RootElement.GetProperty("access_token").GetString();
        }


        public async Task<string> CreateOrderAsync(decimal amount)
        {
            var token = await GetAccessTokenAsync();
            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new { amount = new {
                        currency_code = Currency,
                        value = amount.ToString("0.00", CultureInfo.InvariantCulture)
                    }}
                }
            };

            var req = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/v2/checkout/orders");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            req.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();
            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            return doc.RootElement.GetProperty("id").GetString();
        }

        
        public async Task<PaymentResultDto> ProcessPaymentAsync(PaymentRequestDto request)
        {
            var token = await GetAccessTokenAsync();
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"{BaseUrl}/v2/checkout/orders/{request.OrderId}/capture");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            req.Content = new StringContent("{}", Encoding.UTF8, "application/json");

            var res = await _http.SendAsync(req);
            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            var root = doc.RootElement;

            var status = root.TryGetProperty("status", out var s) ? s.GetString() : "UNKNOWN";
            var approved = status == "COMPLETED";

            return new PaymentResultDto
            {
                Approved = approved,
                Reference = root.TryGetProperty("id", out var id) ? id.GetString() : request.OrderId,
                Message = approved
                    ? "Pago aprobado y capturado con PayPal."
                    : $"El pago no se completó (estado: {status})."
            };
        }
    }
}
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace SalesDashBoardApplication.Services
{
    public class PayPalService
    {
        private readonly PayPalHttpClient _client;

        public PayPalService(IConfiguration configuration)
        {
            var environment = new SandboxEnvironment(
                configuration["PayPal:ClientId"],
                configuration["PayPal:ClientSecret"]
                );
            _client = new PayPalHttpClient( environment );
        }


        public async Task<string> CreateOrder(double amount)
        {
            var order = new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = amount.ToString("F2")
                    }
                }
            }
            };

            var request = new OrdersCreateRequest();
            request.RequestBody(order);

            var response = await _client.Execute(request);
            var result = response.Result<Order>();

            return result.Id;
        }



        public async Task<PayPalHttp.HttpResponse> CaptureOrder(OrdersCaptureRequest request)
        {
            var response = await _client.Execute(request);
            return response;            
        }




       
    }
}

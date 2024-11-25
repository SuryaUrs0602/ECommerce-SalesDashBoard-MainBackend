using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Orders;
using SalesDashBoardApplication.Services;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalsController : ControllerBase
    {
        private readonly PayPalService _payPalService;
        private readonly ILogger<PayPalsController> _logger;


        public PayPalsController(PayPalService payPalService, ILogger<PayPalsController> logger)
        {
            _payPalService = payPalService;
            _logger = logger;
        }




        [HttpPost("create-order")]
        public async Task<string> InitialiseOrder([FromBody] double amount)
        {
            var orderId = await _payPalService.CreateOrder(amount);
            return JsonConvert.SerializeObject(orderId);
        }

                                    


        [HttpPost("capture-order/{orderId}")]
        public async Task<string> CaptureOrder(string orderId)
        {
            try
            {
                var request = new OrdersCaptureRequest(orderId);

                request.RequestBody(new OrderActionRequest());

                var response = await _payPalService.CaptureOrder(request); 

                string confirmMessage = "Order confirmed Successfully";
                string failureMessage = "Failed to confirm the order";

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return JsonConvert.SerializeObject(confirmMessage);
                }
                else
                {
                    return JsonConvert.SerializeObject(failureMessage);
                }


                
            }

            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }
    }
}

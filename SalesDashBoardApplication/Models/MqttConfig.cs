namespace SalesDashBoardApplication.Models
{
    public class MqttConfig
    {
        public string BrokerHost { get; set; } = "localhost";
        public int BrokerPort { get; set; } = 1883;
        public string ClientId { get; set; } = $"ecommerce_dashboard_{Guid.NewGuid()}";
    }
}

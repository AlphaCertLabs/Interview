namespace CanWeFixItService.Models
{
    public class MarketData
    {
        public int Id { get; set; }
        public long? DataValue { get; set; }
        public string Sedol { get; set; }
        public bool Active { get; set; }
    }
}
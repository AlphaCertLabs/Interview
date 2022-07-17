namespace CanWeFixItService.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Sedol { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
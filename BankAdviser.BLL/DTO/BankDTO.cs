namespace BankAdviser.BLL.DTO
{
    public class BankDTO
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Type { get; set; }
        public int AssetsRank { get; set; }
        public int SustainabilityRating { get; set; }
    }
}
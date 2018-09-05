namespace BankAdviser.DAL.Entities
{
    public static class SortBy
    {
        public static string State { get; } = "Profitability";        
        public static string Foreign { get; } = "BanksRating";
        public static string Private { get; } = "BanksAssets";
    }
}
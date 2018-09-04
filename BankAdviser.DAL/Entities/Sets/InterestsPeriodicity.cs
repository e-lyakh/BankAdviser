namespace BankAdviser.DAL.Entities
{
    public class InterestsPeriodicity
    {
        public static string Weekly { get; } = "Weekly";
        public static string Monthly { get; } = "Monthly";
        public static string Quarterly { get; } = "Quarterly";
        public static string OnCompletion { get; } = "OnCompletion";
    }
}
namespace BankAdviser.BLL.DTO
{
    public class ReplyEntryDTO
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string DepositName { get; set; }
        public double DepositRate { get; set; }
        public string DepositBonusInfo { get; set; }
        public double NetIncome { get; set; }
        public string Remark { get; set; }
        public string DepositUrl { get; set; }
    }
}
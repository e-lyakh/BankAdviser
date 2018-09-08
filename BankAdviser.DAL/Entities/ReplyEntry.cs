using System;

namespace BankAdviser.DAL.Entities
{
    public class ReplyEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }        
        public int? DepositId { get; set; }        
        public double NetIncome { get; set; }       
    }
}
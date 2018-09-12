using System;

namespace BankAdviser.DAL.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public DateTime SearchDate { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int AssetsRank { get; set; }
        public int Rating { get; set; }
    }    
}
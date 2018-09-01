using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAdviser.DAL.Entities
{
    public class BankInfo
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BankType { get; set; }
        public int AssetsRating { get; set; }
        public DateTime SearchDate { get; set; }
    }
}

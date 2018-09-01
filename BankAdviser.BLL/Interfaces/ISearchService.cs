using BankAdviser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Interfaces
{
    public interface ISearchService
    {
        void StartSearch(Inquiry inquiry);
    }
}

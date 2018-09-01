using BankAdviser.DAL.Entities;
using System;

namespace BankAdviser.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Dialog> Dialogs { get; }
        IRepository<Inquiry> Inquiries { get; }
        IRepository<DepositInfo> Searches { get; }
        IRepository<Reply> Replies { get; }
        void Save();
    }
}
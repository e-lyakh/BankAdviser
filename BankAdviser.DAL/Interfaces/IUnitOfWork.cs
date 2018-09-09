﻿using BankAdviser.DAL.Entities;
using System;

namespace BankAdviser.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {        
        IRepository<Enquiry> Enquiries { get; }
        IRepository<Bank> Banks { get; }
        IRepository<Deposit> Deposits { get; }
        IRepository<ReplyEntry> ReplyEntries { get; }
        void Save();
    }
}
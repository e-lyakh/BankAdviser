using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;

namespace BankAdviser.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private RDSContext db;
        private EnquiriesRepository enquiriesRepository;
        private BanksRepository banksRepository;        
        private DepositsRepository depositsRepository;
        private ReplyEntriesRepository replyEntriesRepository;

        public UnitOfWork()
        {
            db = new RDSContext();
        }
        
        public IRepository<Enquiry> Enquiries
        {
            get
            {
                if (enquiriesRepository == null)
                    enquiriesRepository = new EnquiriesRepository(db);
                return enquiriesRepository;
            }
        }

        public IRepository<Bank> Banks
        {
            get
            {
                if (banksRepository == null)
                    banksRepository = new BanksRepository(db);
                return banksRepository;
            }
        }

        public IRepository<Deposit> Deposits
        {
            get
            {
                if (depositsRepository == null)
                    depositsRepository = new DepositsRepository(db);
                return depositsRepository;
            }
        }

        public IRepository<ReplyEntry> ReplyEntries
        {
            get
            {
                if (replyEntriesRepository == null)
                    replyEntriesRepository = new ReplyEntriesRepository(db);
                return replyEntriesRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

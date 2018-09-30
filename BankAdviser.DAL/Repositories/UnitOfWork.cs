using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;

namespace BankAdviser.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private RDSContext db;

        private InquiriesRepository inquiriesRepository;
        private BanksRepository banksRepository;        
        private DepositsRepository depositsRepository;
        private ReplyEntriesRepository replyEntriesRepository;
        private RatingsRepository ratingsRepository;

        public UnitOfWork()
        {
            db = new RDSContext();
        }

        public UnitOfWork(string connectionString)
        {
            db = new RDSContext(connectionString);
        }

        public IRepository<Inquiry> Inquiries
        {
            get
            {
                if (inquiriesRepository == null)
                    inquiriesRepository = new InquiriesRepository(db);
                return inquiriesRepository;
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

        public IRepository<Rating> Ratings
        {
            get
            {
                if (ratingsRepository == null)
                    ratingsRepository = new RatingsRepository(db);
                return ratingsRepository;
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
using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;

namespace BankAdviser.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private RDSContext db;
        private DialogRepository dialogRepository;
        private InquiryRepository inquiryRepository;
        private SearchRepository searchRepository;
        private ReplyRepository replyRepository;

        public EFUnitOfWork()
        {
            db = new RDSContext();
        }
        public IRepository<Dialog> Dialogs
        {
            get
            {
                if (dialogRepository == null)
                    dialogRepository = new DialogRepository(db);
                return dialogRepository;
            }
        }

        public IRepository<Inquiry> Inquiries
        {
            get
            {
                if (inquiryRepository == null)
                    inquiryRepository = new InquiryRepository(db);
                return inquiryRepository;
            }
        }

        public IRepository<DepositInfo> Searches
        {
            get
            {
                if (searchRepository == null)
                    searchRepository = new SearchRepository(db);
                return searchRepository;
            }
        }

        public IRepository<Reply> Replies
        {
            get
            {
                if (replyRepository == null)
                    replyRepository = new ReplyRepository(db);
                return replyRepository;
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

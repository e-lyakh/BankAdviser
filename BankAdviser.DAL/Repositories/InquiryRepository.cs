using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class InquiryRepository : IRepository<Inquiry>
    {
        private RDSContext db;

        public InquiryRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Inquiry> GetAll()
        {
            return db.Inquiries;
        }

        public Inquiry Get(int id)
        {
            return db.Inquiries.Find(id);
        }

        public void Create(Inquiry request)
        {
            db.Inquiries.Add(request);
        }

        public void Update(Inquiry request)
        {
            db.Entry(request).State = EntityState.Modified;
        }

        public IEnumerable<Inquiry> Find(Func<Inquiry, Boolean> predicate)
        {
            return db.Inquiries.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Inquiry request = db.Inquiries.Find(id);
            if (request != null)
                db.Inquiries.Remove(request);
        }
    }
}

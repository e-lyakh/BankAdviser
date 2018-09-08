using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class InquiriesRepository : IRepository<Inquiry>
    {
        private RDSContext db;

        public InquiriesRepository(RDSContext db)
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

        public void Create(Inquiry inquiry)
        {            
            db.Inquiries.Add(inquiry);
        }

        public void Update(Inquiry inquiry)
        {
            db.Entry(inquiry).State = EntityState.Modified;
        }

        public IEnumerable<Inquiry> Find(Func<Inquiry, bool> predicate)
        {
            return db.Inquiries.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Inquiry inquiry = db.Inquiries.Find(id);
            if (inquiry != null)
                db.Inquiries.Remove(inquiry);
        }
    }
}

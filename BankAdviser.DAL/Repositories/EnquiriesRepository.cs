using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class EnquiriesRepository : IRepository<Enquiry>
    {
        private RDSContext db;

        public EnquiriesRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Enquiry> GetAll()
        {
            return db.Enquiries;
        }

        public Enquiry Get(int id)
        {
            return db.Enquiries.Find(id);
        }

        public void Create(Enquiry enquiry)
        {            
            db.Enquiries.Add(enquiry);
        }

        public void Update(Enquiry enquiry)
        {
            db.Entry(enquiry).State = EntityState.Modified;
        }

        public IEnumerable<Enquiry> Find(Func<Enquiry, bool> predicate)
        {
            return db.Enquiries.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Enquiry enquiry = db.Enquiries.Find(id);
            if (enquiry != null)
                db.Enquiries.Remove(enquiry);
        }
    }
}

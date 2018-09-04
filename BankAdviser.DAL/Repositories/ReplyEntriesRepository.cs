using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    class ReplyEntriesRepository : IRepository<ReplyEntry>
    {
        private RDSContext db;

        public ReplyEntriesRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<ReplyEntry> GetAll()
        {
            return db.ReplyEntries;
        }

        public ReplyEntry Get(int id)
        {
            return db.ReplyEntries.Find(id);
        }

        public void Create(ReplyEntry replyEntry)
        {
            db.ReplyEntries.Add(replyEntry);
        }

        public void Update(ReplyEntry replyEntry)
        {
            db.Entry(replyEntry).State = EntityState.Modified;
        }

        public IEnumerable<ReplyEntry> Find(Func<ReplyEntry, bool> predicate)
        {
            return db.ReplyEntries.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ReplyEntry replyEntry = db.ReplyEntries.Find(id);
            if (replyEntry != null)
                db.ReplyEntries.Remove(replyEntry);
        }
    }
}
using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    class ReplyRepository : IRepository<Reply>
    {
        private RDSContext db;

        public ReplyRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Reply> GetAll()
        {
            return db.Responses;
        }

        public Reply Get(int id)
        {
            return db.Responses.Find(id);
        }

        public void Create(Reply response)
        {
            db.Responses.Add(response);
        }

        public void Update(Reply response)
        {
            db.Entry(response).State = EntityState.Modified;
        }

        public IEnumerable<Reply> Find(Func<Reply, Boolean> predicate)
        {
            return db.Responses.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Reply response = db.Responses.Find(id);
            if (response != null)
                db.Responses.Remove(response);
        }
    }
}

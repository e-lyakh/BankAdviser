using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class SearchRepository : IRepository<DepositInfo>
    {
        private RDSContext db;

        public SearchRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<DepositInfo> GetAll()
        {
            return db.Searches;
        }

        public DepositInfo Get(int id)
        {
            return db.Searches.Find(id);
        }

        public void Create(DepositInfo search)
        {
            db.Searches.Add(search);
        }

        public void Update(DepositInfo search)
        {
            db.Entry(search).State = EntityState.Modified;
        }

        public IEnumerable<DepositInfo> Find(Func<DepositInfo, Boolean> predicate)
        {
            return db.Searches.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            DepositInfo search = db.Searches.Find(id);
            if (search != null)
                db.Searches.Remove(search);
        }
    }
}

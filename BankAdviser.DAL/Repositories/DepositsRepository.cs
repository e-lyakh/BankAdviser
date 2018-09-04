using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class DepositsRepository : IRepository<Deposit>
    {
        private RDSContext db;

        public DepositsRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Deposit> GetAll()
        {
            return db.Deposits;
        }

        public Deposit Get(int id)
        {
            return db.Deposits.Find(id);
        }

        public void Create(Deposit deposit)
        {            
            db.Deposits.Add(deposit);
        }

        public void Update(Deposit deposit)
        {
            db.Entry(deposit).State = EntityState.Modified;
        }

        public IEnumerable<Deposit> Find(Func<Deposit, bool> predicate)
        {
            return db.Deposits.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Deposit deposit = db.Deposits.Find(id);
            if (deposit != null)
                db.Deposits.Remove(deposit);
        }
    }
}
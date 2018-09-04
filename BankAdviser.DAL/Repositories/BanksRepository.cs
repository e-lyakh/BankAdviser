using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class BanksRepository : IRepository<Bank>
    {
        private RDSContext db;

        public BanksRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Bank> GetAll()
        {
            return db.Banks;
        }

        public Bank Get(int id)
        {
            return db.Banks.Find(id);
        }

        public void Create(Bank bank)
        {
            bank.SearchDate = DateTime.Now;
            db.Banks.Add(bank);
        }

        public void Update(Bank bank)
        {
            db.Entry(bank).State = EntityState.Modified;
        }

        public IEnumerable<Bank> Find(Func<Bank, bool> predicate)
        {
            return db.Banks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Bank bank = db.Banks.Find(id);
            if (bank != null)
                db.Banks.Remove(bank);
        }
    }
}
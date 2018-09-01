using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class DialogRepository : IRepository<Dialog>
    {
        private RDSContext db;

        public DialogRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Dialog> GetAll()
        {
            return db.Dialogs;
        }

        public Dialog Get(int id)
        {
            return db.Dialogs.Find(id);
        }

        public void Create(Dialog dialog)
        {
            db.Dialogs.Add(dialog);
        }

        public void Update(Dialog dialog)
        {
            db.Entry(dialog).State = EntityState.Modified;
        }

        public IEnumerable<Dialog> Find(Func<Dialog, bool> predicate)
        {
            return db.Dialogs.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog != null)
                db.Dialogs.Remove(dialog);
        }
    }
}
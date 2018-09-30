using BankAdviser.DAL.EF;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankAdviser.DAL.Repositories
{
    public class RatingsRepository : IRepository<Rating>
    {
        private RDSContext db;

        public RatingsRepository(RDSContext db)
        {
            this.db = db;
        }

        public IEnumerable<Rating> GetAll()
        {
            return db.Ratings;
        }

        public Rating Get(int id)
        {
            return db.Ratings.Find(id);
        }

        public void Create(Rating rating)
        {
            rating.SearchDate = DateTime.Now;
            db.Ratings.Add(rating);
        }

        public void Update(Rating rating)
        {
            db.Entry(rating).State = EntityState.Modified;
        }

        public IEnumerable<Rating> Find(Func<Rating, bool> predicate)
        {
            return db.Ratings.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating != null)
                db.Ratings.Remove(rating);
        }
    }
}
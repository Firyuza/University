namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Castle.Windsor;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;
    using Storage;

    public class PositionService : IPositionService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public PositionService()
        {
            db = new ApplicationDbContext();
        }

        public IList<Position> GetAll()
        {
            return db.Positions.ToList();
        }

        public Position Get(long id)
        {
            return db.Positions.Find(id);
        }

        public void Add(Position position)
        {
            db.Positions.Add(position);

            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var position = db.Positions.Find(id);

            db.Positions.Remove(position);

            db.SaveChanges();
        }

        public void Edit(Position position)
        {
            db.Entry(position).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

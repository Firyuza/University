using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Castle.Windsor;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;

namespace BusinessLogic
{
    public class GroupService : IGroupService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public GroupService()
        {
            db = new ApplicationDbContext();
        }

        public IList<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public Group Get(long id)
        {
            return db.Groups.Find(id);
        }

        public void Add(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var group = db.Groups.Find(id);

            db.Groups.Remove(group);
            db.SaveChanges();
        }

        public void Edit(Group group)
        {
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

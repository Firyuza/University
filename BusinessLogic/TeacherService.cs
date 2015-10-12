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

    public class TeacherService : ITeacherService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public TeacherService()
        {
            db = new ApplicationDbContext();
        }
        public IList<Teacher> GetAll()
        {
            return db.Teachers.ToList();
        }

        public Teacher Get(long id)
        {
            return db.Teachers.Find(id);
        }

        public void Add(Teacher teacher)
        {
            SetRelativeEntities(teacher);

            db.Teachers.Add(teacher);
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var teacher = db.Teachers.Find(id);

            if (teacher != null)
            {
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
        }

        public void Edit(Teacher teacher)
        {
            SetRelativeEntities(teacher);

            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetRelativeEntities(Teacher teacher)
        {
            var department = db.Departments.Find(teacher.Department.id);
            teacher.Department = department;

            var position = db.Positions.Find(teacher.Position.id);
            teacher.Position = position;
        }
    }
}

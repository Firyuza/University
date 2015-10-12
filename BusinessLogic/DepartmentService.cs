using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;

namespace BusinessLogic
{
    public class DepartmentService : IDepartmentService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public DepartmentService()
        {
            db = new ApplicationDbContext();
        }

        public IList<Department> GetAll()
        {
            return db.Departments.ToList();
        }

        public Department Get(long id)
        {
            return db.Departments.Find(id);
        }

        public void Add(Department department)
        {
            db.Departments.Add(department);

            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var department = db.Departments.Find(id);

            if (department != null)
            {
                db.Departments.Remove(department);

                db.SaveChanges();
            }
        }

        public void Edit(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

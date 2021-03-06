﻿using System;
using System.Data.Entity;
using Castle.Windsor;

namespace BusinessLogic
{
    using System.Collections.Generic;
    using System.Linq;
    using Shared.Models.Entities;
    using Storage;
    using Shared.Models.Interfaces;

    public class StudentService : IStudentService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public StudentService()
        {
            db = new ApplicationDbContext();
        }
        public IList<Student> GetAll()
        {
            return db.Students.ToList();
        }

        public Student Get(long id)
        {
            return db.Students.Find(id);
        }

        public void Add(Student student)
        {
            SetRelativeEntities(student);

            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var student = db.Students.Find(id);

            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public void Edit(Student student)
        {
            SetRelativeEntities(student);

            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IQueryable<Student> GetByGroup(long id)
        {
            return db.Students
                .Where(x => x.Group.id == id);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetRelativeEntities(Student student)
        {
            var group = db.Groups.Find(student.Group.id);
            student.Group = group;
        }
    }
}

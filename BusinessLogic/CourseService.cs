using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;

namespace BusinessLogic
{
    public class CourseService : ICourseService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public CourseService()
        {
            db = new ApplicationDbContext();
        }

        public IList<Course> GetAll()
        {
            return db.Courses.ToList();
        }

        public Course Get(long id)
        {
            return db.Courses.Find(id);
        }

        public void Add(Course course)
        {
            SetRelativeEntities(course);
            db.Teachers.Attach(course.Teacher);

            db.Courses.Add(course);

            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var course = db.Courses.Find(id);

            db.Courses.Remove(course);

            db.SaveChanges();
        }

        public void Edit(Course course)
        {
            var editingCourse = db.Courses.Find(course.id);

            SetRelativeEntities(editingCourse, course);
            editingCourse.name = course.name;

            db.Courses.AddOrUpdate(editingCourse);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetRelativeEntities(Course oldCourse, Course newCourse)
        {
            var teacher = db.Teachers.Find(newCourse.Teacher.id);
            oldCourse.Teacher = teacher;
        }

        private void SetRelativeEntities(Course course)
        {
            var teacher = db.Teachers.Find(course.Teacher.id);
            course.Teacher = teacher;
        }
    }
}

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
    public class AcademicProgressService : IAcademicProgressService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public AcademicProgressService()
        {
            db = new ApplicationDbContext();
        }

        public IList<AcademicProgress> GetAll()
        {
            return db.AcademicProgresses.ToList();
        }

        public AcademicProgress Get(long id)
        {
            return db.AcademicProgresses.Find(id);
        }

        public void Add(AcademicProgress academicProgress)
        {
            SetRelativeEntities(academicProgress);

            db.Students.Attach(academicProgress.Student);
            db.Teachers.Attach(academicProgress.Course.Teacher);

            db.AcademicProgresses.Add(academicProgress);
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var academicProgress = db.AcademicProgresses.Find(id);
            db.AcademicProgresses.Remove(academicProgress);
            db.SaveChanges();
        }

        public void Edit(AcademicProgress academicProgress)
        {
            var editingProgress = db.AcademicProgresses.Find(academicProgress.id);

            SetRelativeEntities(editingProgress, academicProgress);
            editingProgress.date = academicProgress.date;
            editingProgress.score = academicProgress.score;

            db.AcademicProgresses.AddOrUpdate(editingProgress);

            db.SaveChanges();
        }

        public IQueryable<AcademicProgress> GetByGroupCourse(long gId, long cId)
        {
            return db.AcademicProgresses
                .Where(
                    x =>
                        x.Student.Group.id == gId &&
                        x.Course.id == cId);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetRelativeEntities(AcademicProgress oldProgress, AcademicProgress newProgress)
        {
            var group = db.Students.Find(newProgress.Student.id);
            oldProgress.Student = group;

            var teacher = db.Teachers.Find(newProgress.Course.Teacher.id);
            oldProgress.Course.Teacher = teacher;
        }

        private void SetRelativeEntities(AcademicProgress academicProgress)
        {
            var student = db.Students.Find(academicProgress.Student.id);
            academicProgress.Student = student;

            var course = db.Courses.Find(academicProgress.Course.id);
            academicProgress.Course = course;
        }
    }
}

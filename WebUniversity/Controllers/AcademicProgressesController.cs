using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class AcademicProgressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AcademicProgresses
        public ActionResult Index()
        {
            return View(db.AcademicProgresses.ToList());
        }

        // GET: AcademicProgresses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgress academicProgress = db.AcademicProgresses.Find(id);
            if (academicProgress == null)
            {
                return HttpNotFound();
            }
            return View(academicProgress);
        }

        // GET: AcademicProgresses/Create
        public ActionResult Create()
        {
            GetRelativeEntities();

            return View();
        }

        // POST: AcademicProgresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,score,date, teacher, student")] AcademicProgress academicProgress)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(academicProgress);

                db.Students.Attach(academicProgress.Student);
                db.Teachers.Attach(academicProgress.Teacher);

                db.AcademicProgresses.Add(academicProgress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicProgress);
        }

        // GET: AcademicProgresses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgress academicProgress = db.AcademicProgresses.Find(id);
            if (academicProgress == null)
            {
                return HttpNotFound();
            }
            return View(academicProgress);
        }

        // POST: AcademicProgresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,score,date, teacher, student")] AcademicProgress academicProgress)
        {
            if (ModelState.IsValid)
            {
                var editingProgress = db.AcademicProgresses.Find(academicProgress.id);

                SetRelativeEntities(editingProgress, academicProgress);
                editingProgress.date = academicProgress.date;
                editingProgress.score = academicProgress.score;

                db.AcademicProgresses.AddOrUpdate(editingProgress);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicProgress);
        }

        // GET: AcademicProgresses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgress academicProgress = db.AcademicProgresses.Find(id);
            if (academicProgress == null)
            {
                return HttpNotFound();
            }
            return View(academicProgress);
        }

        // POST: AcademicProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AcademicProgress academicProgress = db.AcademicProgresses.Find(id);
            db.AcademicProgresses.Remove(academicProgress);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetStudentsByGroup(long id)
        {
            var students = db.Students
                .Where(x => x.Group.id == id)
                .Select(s => new
                {
                    s.id,
                    name = s.Person.firstname + " " + s.Person.lastname + " " + s.Person.middlename
                })
                .ToList();

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTeachersByCourse(long id)
        {
            var teachers = db.Teachers
                .Where(x => x.Course.id == id)
                .Select(s => new
                {
                    s.id,
                    name = s.Person.firstname + " " + s.Person.lastname + " " + s.Person.middlename
                })
                .ToList();

            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetRelativeEntities()
        {
            var courses = db.Courses.ToList();
            ViewBag.Course = new SelectList(courses, "id", "name");
        }

        private void SetRelativeEntities(AcademicProgress oldProgress, AcademicProgress newProgress)
        {
            var group = db.Students.Find(newProgress.Student.id);
            oldProgress.Student = group;

            var teacher = db.Teachers.Find(newProgress.Teacher.id);
            oldProgress.Teacher = teacher;
        }

        private void SetRelativeEntities(AcademicProgress academicProgress)
        {
            var student = db.Students.Find(academicProgress.Student.id);
            academicProgress.Student = student;

            var teacher = db.Teachers.Find(academicProgress.Teacher.id);
            academicProgress.Teacher = teacher;
        }
    }
}

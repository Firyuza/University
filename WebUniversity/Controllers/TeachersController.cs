using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shared.Models.Entities;
using Storage;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(long? id)
        {
            GetRelativeEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            GetRelativeEntities();

            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Person,Department,Course,Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(teacher);

                db.Teachers.Add(teacher);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            GetRelativeEntities();

            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Person,Department,Course,Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(teacher);

                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            var departments = db.Departments.ToList();
            ViewBag.Department = new SelectList(departments, "id", "name");

            var positions = db.Positions.ToList();
            ViewBag.Position = new SelectList(positions, "id", "name");

            var courses = db.Courses.ToList();
            ViewBag.Course = new SelectList(courses, "id", "name");
        }

        private void SetRelativeEntities(Teacher teacher)
        {
            var department = db.Departments.Find(teacher.Department.id);
            teacher.Department = department;

            var position = db.Positions.Find(teacher.Position.id);
            teacher.Position = position;

            var course = db.Courses.Find(teacher.Course.id);
            teacher.Course = course;
        }
    }
}

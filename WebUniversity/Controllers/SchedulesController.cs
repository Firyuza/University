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
    public class SchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedules
        public ActionResult Index()
        {
            GetRelativeEntities();

            return View(db.Schedules.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            GetRelativeEntities();

            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,day, Group, Teacher")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(schedule);

                db.Groups.Attach(schedule.Group);
                db.Teachers.Attach(schedule.Teacher);

                db.Schedules.Add(schedule);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }

            GetRelativeEntities();

            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,day, Group, Teacher")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var editingSchedule = db.Schedules.Find(schedule.id);

                SetRelativeEntities(editingSchedule, schedule);
                editingSchedule.day = schedule.day;
                
                db.Schedules.AddOrUpdate(editingSchedule);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
            var groups = db.Groups
                .ToList();
            ViewBag.Group = new SelectList(groups, "id", "name");

            var courses = db.Courses
               .ToList();
            ViewBag.Course = new SelectList(courses, "id", "name");

            var teachers = db.Teachers
                .ToList()
                .Select(s => new
                {
                    s.id,
                    name = s.Person.firstname + " " + s.Person.lastname + " " + s.Person.middlename
                });
            ViewBag.Teacher = new SelectList(teachers, "id", "name");
        }

        private void SetRelativeEntities(Schedule oldSchedule, Schedule newSchedule)
        {
            var group = db.Groups.Find(newSchedule.Group.id);
            oldSchedule.Group = group;

            var teacher = db.Teachers.Find(newSchedule.Teacher.id);
            oldSchedule.Teacher = teacher;
        }

        private void SetRelativeEntities(Schedule schedule)
        {
            var group = db.Groups.Find(schedule.Group.id);
            schedule.Group = group;

            var teacher = db.Teachers.Find(schedule.Teacher.id);
            schedule.Teacher = teacher;
        }
    }
}

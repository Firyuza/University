namespace WebUniversity.Controllers
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Storage;

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
        public ActionResult Create([Bind(Include = "id,day, Group, Course")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(schedule);

                db.Groups.Attach(schedule.Group);
                db.Courses.Attach(schedule.Course);

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

        public ActionResult GetTeachersByCourse(long id)
        {
            var teachers = db.Courses
                .Where(x => x.id == id)
                .Select(s => new
                {
                    s.id,
                    name = s.Teacher.Person.firstname + " " + s.Teacher.Person.lastname + " " + s.Teacher.Person.middlename
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
            var groups = db.Groups
                .ToList();
            ViewBag.Group = new SelectList(groups, "id", "name");

            var courses = db.Courses
                .Select(s => new Course()
                {
                    id = s.id,
                    name = s.name + " - " + s.Teacher.Person.lastname + " " + s.Teacher.Person.firstname
                })
               .ToList();

            courses.Insert(0, new Course()
            {
                name = "--Select--"
            });
            ViewBag.Course = new SelectList(courses, "id", "name");
        }

        private void SetRelativeEntities(Schedule oldSchedule, Schedule newSchedule)
        {
            var group = db.Groups.Find(newSchedule.Group.id);
            oldSchedule.Group = group;

            var course = db.Courses.Find(newSchedule.Course.id);
            oldSchedule.Course = course;
        }

        private void SetRelativeEntities(Schedule schedule)
        {
            var group = db.Groups.Find(schedule.Group.id);
            schedule.Group = group;

            var course = db.Courses.Find(schedule.Course.id);
            schedule.Course = course;
        }
    }
}

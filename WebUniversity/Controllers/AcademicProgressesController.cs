namespace WebUniversity.Controllers
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Storage;

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
                db.Teachers.Attach(academicProgress.Course.Teacher);

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

        public ActionResult GetCoursesByGroup(long id)
        {
            var courses = db.Schedules
                .Where(x => x.Group.id == id)
                .Select(s => new 
                {
                    id = s.Course.id,
                    name = s.Course.name + " - " + s.Course.Teacher.Person.lastname + " " + s.Course.Teacher.Person.firstname
                })
                .ToList();

            return Json(courses, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Shared.Models.Entities;
using Storage;

namespace WebUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            FillViewBag();

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name, Teacher")] Course course)
        {
            if (ModelState.IsValid)
            {
                SetRelativeEntities(course);
                db.Teachers.Attach(course.Teacher);

                db.Courses.Add(course);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            FillViewBag();

            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Course course)
        {
            if (ModelState.IsValid)
            {
                var editingCourse = db.Courses.Find(course.id);

                SetRelativeEntities(editingCourse, course);
                editingCourse.name = course.name;

                db.Courses.AddOrUpdate(editingCourse);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        public void FillViewBag()
        {
            var teachers = db.Teachers
                .Select(s => new 
                {
                    s.id,
                    name = s.Person.lastname + " " + s.Person.firstname
                })
                .ToList();

            ViewBag.Teachers = new SelectList(teachers, "id", "name");
        }

        private void SetRelativeEntities(Course course)
        {
            var teacher = db.Teachers.Find(course.Teacher.id);
            course.Teacher = teacher;
        }

        private void SetRelativeEntities(Course oldCourse, Course newCourse)
        {
            var teacher = db.Teachers.Find(newCourse.Teacher.id);
            oldCourse.Teacher = teacher;
        }
    }
}

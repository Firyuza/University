using System.Linq;

namespace WebUniversity.Controllers
{
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;

    public class CoursesController : Controller
    {
        private ICourseService courseService;
        private ITeacherService teacherService;

        public CoursesController(ICourseService cs, ITeacherService ts)
        {
            courseService = cs;
            teacherService = ts;
        }

        // GET: Courses
        public ActionResult Index()
        {
            return View(courseService.GetAll());
        }

        // GET: Courses/Details/5
        public ActionResult Details(long id)
        {
            Course course = courseService.Get(id);

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
                courseService.Add(course);

                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(long id)
        {
            Course course = courseService.Get(id);

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
                courseService.Edit(course);

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(long id)
        {
            Course course = courseService.Get(id);

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
            courseService.Remove(id);

            return RedirectToAction("Index");
        }

        public void FillViewBag()
        {
            var teachers = teacherService.GetAll()
                .Select(s => new 
                {
                    s.id,
                    name = s.Person.lastname + " " + s.Person.firstname
                })
                .ToList();

            ViewBag.Teachers = new SelectList(teachers, "id", "name");
        }
    }
}

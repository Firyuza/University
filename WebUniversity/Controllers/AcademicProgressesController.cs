using Shared.Models.Interfaces;

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
        private IAcademicProgressService academicProgressService;
        private IStudentService studentService;
        private IScheduleService scheduleService;

        public AcademicProgressesController(IAcademicProgressService aps, IStudentService ss, IScheduleService scs)
        {
            academicProgressService = aps;
            studentService = ss;
            scheduleService = scs;
        }

        // GET: AcademicProgresses
        public ActionResult Index()
        {
            return View(academicProgressService.GetAll());
        }

        // GET: AcademicProgresses/Details/5
        public ActionResult Details(long id)
        {
            AcademicProgress academicProgress = academicProgressService.Get(id);

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
        public ActionResult Create([Bind(Include = "id,score,date, course, student")] AcademicProgress academicProgress)
        {
            if (ModelState.IsValid)
            {
                academicProgressService.Add(academicProgress);

                return RedirectToAction("Index");
            }

            return View(academicProgress);
        }

        // GET: AcademicProgresses/Edit/5
        public ActionResult Edit(long id)
        {
            AcademicProgress academicProgress = academicProgressService.Get(id);

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
                academicProgressService.Add(academicProgress);

                return RedirectToAction("Index");
            }
            return View(academicProgress);
        }

        // GET: AcademicProgresses/Delete/5
        public ActionResult Delete(long id)
        {
            AcademicProgress academicProgress = academicProgressService.Get(id);

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
            academicProgressService.Remove(id);

            return RedirectToAction("Index");
        }

        public ActionResult GetStudentsByGroup(long id)
        {
            var students = studentService.GetByGroup(id)
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
            var courses = scheduleService.GetByGroup(id)
                .Select(s => new 
                {
                    s.Course.id,
                    name = s.Course.name + " - " + s.Course.Teacher.Person.lastname + " " + s.Course.Teacher.Person.firstname
                })
                .ToList();

            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}

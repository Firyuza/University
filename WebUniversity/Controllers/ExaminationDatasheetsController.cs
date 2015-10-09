namespace WebUniversity.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Storage;
    using ViewModel;

    public class ExaminationDatasheetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExaminationDatasheets
        public ActionResult Index()
        {
            return View();
        }

        // POST: ExaminationDatasheets/Create
        [HttpPost]
        public ActionResult CreateReport([Bind(Include = "id,GroupId,CourseId,TeacherId")] ExaminationDatasheetsViewModel model)
        {
            try
            {
                var report = new ExaminationDatasheet();
                
                report.Group = db.Groups.Find(model.GroupId);
                report.Course = db.Courses.Find(model.CourseId);
                
                report.AcademicProgresses =
                    db.AcademicProgresses
                    .Where(
                        x =>
                            x.Student.Group.id == model.GroupId && 
                            x.Course.id == model.CourseId)
                            .ToList();

                if (report.AcademicProgresses.Count() != 0)
                {
                    double? sum = 0;
                    foreach (var s in report.AcademicProgresses)
                    {
                        var d = s.score;
                        if (d.HasValue)
                            sum += d.Value;
                    }
                    report.AvarageScore = sum.Value/
                                          report.AcademicProgresses.Count();
                }
                
                return View("Report", report);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult GetGroups()
        {
            var groups = db.Groups
                .ToList()
                .Select(s => new
                {
                    GroupId = s.id,
                    Name = s.name
                }).ToList();

            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCoursesByGroup(long id)
        {
            var courses = db.Schedules
                .Where(x => x.Group.id == id)
                .Include(s => s.Course)
                .Select(s => new
                {
                    CourseId = s.Course.id,
                    Name = s.Course.name + " - " + s.Course.Teacher.Person.firstname + " " + s.Course.Teacher.Person.lastname + " " + s.Course.Teacher.Person.middlename
                })
                .ToList();

            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}

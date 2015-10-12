namespace WebUniversity.Controllers
{
    using Shared.Models.Interfaces;
    using System.Linq;
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using ViewModel;

    public class ExaminationDatasheetsController : Controller
    {
        private IGroupService groupService;
        private ICourseService courseService;
        private IAcademicProgressService academicProgressService;
        private IScheduleService scheduleService;

        public ExaminationDatasheetsController(IGroupService gs, ICourseService cs, IAcademicProgressService aps,
            IScheduleService scs)
        {
            groupService = gs;
            courseService = cs;
            academicProgressService = aps;
            scheduleService = scs;
        }

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
                
                report.Group = groupService.Get(model.GroupId);
                report.Course = courseService.Get(model.CourseId);
                
                report.AcademicProgresses = academicProgressService.GetByGroupCourse(model.GroupId, model.CourseId).ToList();

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
            var groups = groupService
                .GetAll()
                .Select(s => new
                {
                    GroupId = s.id,
                    Name = s.name
                }).ToList();

            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCoursesByGroup(long id)
        {
            var courses = scheduleService
                .GetByGroup(id)
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

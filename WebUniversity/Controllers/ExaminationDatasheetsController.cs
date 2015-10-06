using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor;
using WebUniversity.Models;
using WebUniversity.ViewModel;

namespace WebUniversity.Controllers
{
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
                report.Teacher = db.Teachers.Find(model.TeacherId);

                report.AcademicProgresses =
                    db.AcademicProgresses
                    .Where(
                        x =>
                            x.Student.Group.id == model.GroupId && x.Teacher.Course.id == model.CourseId &&
                            x.Teacher.id == model.TeacherId)
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
                .Include(s => s.Teacher)
                .Select(s => new
                {
                    CourseId = s.Teacher.Course.id,
                    Name = s.Teacher.Course.name,
                    TeacherId = s.Teacher.id,
                    TeacherName = s.Teacher.Person.firstname + " " + s.Teacher.Person.lastname + " " + s.Teacher.Person.middlename
                })
                .ToList();

            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}

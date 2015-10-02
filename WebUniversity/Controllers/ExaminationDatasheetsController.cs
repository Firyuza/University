using System;
using System.Collections.Generic;
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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
            var courses = db.Schedules.Where(x => x.Group.id == id)
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

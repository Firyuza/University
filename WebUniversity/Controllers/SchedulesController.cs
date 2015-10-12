namespace WebUniversity.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;

    public class SchedulesController : Controller
    {
        private IScheduleService scheduleService;
        private IGroupService groupService;
        private ICourseService courseService;

        public SchedulesController(IScheduleService ss, IGroupService gs, ICourseService cs)
        {
            scheduleService = ss;
            groupService = gs;
            courseService = cs;
        }

        // GET: Schedules
        public ActionResult Index()
        {
            GetRelativeEntities();

            return View(scheduleService.GetAll());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(long id)
        {
            var schedule = scheduleService.Get(id);

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
                scheduleService.Add(schedule);

                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(long id)
        {
            Schedule schedule = scheduleService.Get(id);

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
                scheduleService.Edit(schedule);

                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(long id)
        {
            Schedule schedule = scheduleService.Get(id);

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
            scheduleService.Remove(id);

            return RedirectToAction("Index");
        }

        private void GetRelativeEntities()
        {
            var groups = groupService.GetAll();
            ViewBag.Group = new SelectList(groups, "id", "name");

            var courses = courseService.GetAll()
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
    }
}

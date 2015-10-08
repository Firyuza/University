namespace WebUniversity.Controllers
{
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;

    public class StudentsController : Controller
    {
        private IStudentService studentService;
        private IGroupService groupService;

        public StudentsController(IStudentService ss, IGroupService gs)
        {
            studentService = ss;
            groupService = gs;
        }

        // GET: Students
        public ActionResult Index()
        {
            return View(studentService.GetAll());
        }

        // GET: Students/Details/5
        public ActionResult Details(long id)
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            GetGroupList();

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Person,Recordbook,Group")] Student student)
        {
            // Make full Student Entity after posting
            SetRelativeEntities(student);

            studentService.Add(student);
            
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(long id)
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            GetGroupList();

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Person,Recordbook,Group")] Student student)
        {
            if (ModelState.IsValid)
            {
                studentService.Edit(student);
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(long id)
        {
            var student = studentService.Get(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            studentService.Remove(id);
            return RedirectToAction("Index");
        }

        private void SetRelativeEntities(Student student)
        {
            var group = groupService.Get(student.Group.id);
            student.Group = group;
        }

        private void GetGroupList()
        {
            var groups = groupService.GetAll();
            ViewBag.Group = new SelectList(groups, "id", "name");
        }
    }
}

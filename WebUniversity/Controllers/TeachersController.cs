using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class TeachersController : Controller
    {
        private ITeacherService teacherService;
        private IDepartmentService departmentService;
        private IPositionService positionService;

        public TeachersController(ITeacherService ts, IDepartmentService ds, IPositionService ps)
        {
            teacherService = ts;
            departmentService = ds;
            positionService = ps;
        }

        // GET: Teachers
        public ActionResult Index()
        {
            return View(teacherService.GetAll());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(long id)
        {
            Teacher teacher = teacherService.Get(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            GetRelativeEntities();

            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            GetRelativeEntities();

            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Person,Department,Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacherService.Add(teacher);

                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(long id)
        {
            Teacher teacher = teacherService.Get(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            GetRelativeEntities();

            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Person,Department,Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacherService.Edit(teacher);

                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(long id)
        {
            Teacher teacher = teacherService.Get(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            teacherService.Remove(id);

            return RedirectToAction("Index");
        }

        private void GetRelativeEntities()
        {
            var departments = departmentService.GetAll();
            ViewBag.Department = new SelectList(departments, "id", "name");

            var positions = positionService.GetAll();
            ViewBag.Position = new SelectList(positions, "id", "name");
        }
    }
}

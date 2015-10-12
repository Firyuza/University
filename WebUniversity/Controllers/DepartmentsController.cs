using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepartmentService departmentService;

        public DepartmentsController(IDepartmentService ds)
        {
            departmentService = ds;
        }

        // GET: Departments
        public ActionResult Index()
        {
            return View(departmentService.GetAll());
        }

        // GET: Departments/Details/5
        public ActionResult Details(long id)
        {
            Department department = departmentService.Get(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Department department)
        {
            if (ModelState.IsValid)
            {
                departmentService.Add(department);
                
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(long id)
        {
            Department department = departmentService.Get(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Department department)
        {
            if (ModelState.IsValid)
            {
                departmentService.Edit(department);

                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(long id)
        {
            Department department = departmentService.Get(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            departmentService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}

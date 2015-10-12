using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Shared.Models.Entities;
using Shared.Models.Interfaces;
using Storage;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupService groupService;

        public GroupsController(IGroupService gs)
        {
            groupService = gs;
        }
        // GET: Groups
        public ActionResult Index()
        {
            return View(groupService.GetAll());
        }

        // GET: Groups/Details/5
        public ActionResult Details(long id)
        {
            Group group = groupService.Get(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Add(group);

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(long id)
        {
            Group group = groupService.Get(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Edit(group);

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(long id)
        {
            Group group = groupService.Get(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            groupService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}

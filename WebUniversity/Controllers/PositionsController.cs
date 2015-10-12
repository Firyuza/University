namespace WebUniversity.Controllers
{
    using System.Web.Mvc;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;

    public class PositionsController : Controller
    {
        private IPositionService positionService;

        public PositionsController(IPositionService ps)
        {
            positionService = ps;
        }
        // GET: Positions
        public ActionResult Index()
        {
            return View(positionService.GetAll());
        }

        // GET: Positions/Details/5
        public ActionResult Details(long id)
        {
            Position position = positionService.Get(id);

            if (position == null)
            {
                return HttpNotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Position position)
        {
            if (ModelState.IsValid)
            {
                positionService.Add(position);

                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Positions/Edit/5
        public ActionResult Edit(long id)
        {
            Position position = positionService.Get(id);

            if (position == null)
            {
                return HttpNotFound();
            }

            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Position position)
        {
            if (ModelState.IsValid)
            {
                positionService.Edit(position);

                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Positions/Delete/5
        public ActionResult Delete(long id)
        {
            Position position = positionService.Get(id);

            if (position == null)
            {
                return HttpNotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            positionService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}

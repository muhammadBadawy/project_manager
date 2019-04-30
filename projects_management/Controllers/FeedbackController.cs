using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projects_management.Models;

namespace projects_management.Controllers
{
    public class FeedbackController : Controller
    {
        private DBEntities db = new DBEntities();

        // GET: Feedback
        public ActionResult Index()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            var pm_feedback = db.pm_feedback.Where(p => p.evaluated_id == user_id).ToList();
            return View(pm_feedback.ToList());
        }

        // GET: Feedback/Create
        public ActionResult Create(int id)
        {
            ViewBag.evaluator_id = new SelectList(db.pm_User, "id", "firstname");
            ViewBag.evaluated_id = id;
            return View();
        }

        // POST: Feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,evaluator_id,evaluated_id,rating,reason")] pm_feedback pm_feedback_instance)
        {
            if (ModelState.IsValid)
            {
                int user_id = int.Parse(Session["user_id"].ToString());

                pm_feedback_instance.evaluator_id = user_id;
                //pm_feedback_instance.evaluated_id = id;

                db.pm_feedback.Add(pm_feedback_instance);
                db.SaveChanges();

                if (true)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                return RedirectToAction("Index", user_id);
            }

            if (true)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.evaluator_id = new SelectList(db.pm_User, "id", "firstname", pm_feedback_instance.evaluator_id);
            ViewBag.evaluated_id = new SelectList(db.pm_User, "id", "firstname", pm_feedback_instance.evaluated_id);
            return View(pm_feedback_instance);
        }

        public ActionResult Delete(int id)
        {
            pm_feedback pm_feedback = db.pm_feedback.Find(id);
            db.pm_feedback.Remove(pm_feedback);
            db.SaveChanges();

            if (true)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            return RedirectToAction("Index");
        }

    }
}

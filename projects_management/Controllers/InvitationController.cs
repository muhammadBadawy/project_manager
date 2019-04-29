using projects_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projects_management.Controllers
{
    public class InvitationController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Invitation
        public ActionResult Index(int id)
        {
            
            var invitations = db.pm_projectTeam.Where(e => e.member_id == id).Where(e => e.state == 0).ToList();
            return View();

        }

        // GET: Invitation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Invitation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invitation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Invitation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invitation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invitation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invitation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

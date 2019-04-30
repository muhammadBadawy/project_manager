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
        public ActionResult Index()
        {
            int id = int.Parse(Session["user_id"].ToString());
            int user_role_id = int.Parse(Session["user_role_id"].ToString());

            if (user_role_id == 5)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            var invitations = db.pm_projectTeam.Where(e => e.member_id == id).Where(e => e.state == 0).ToList();
            return View(invitations);
        }

        // POST: Invitation/Edit/5
        [HttpGet]
        public ActionResult Approve(int id)
        {
            try
            {

                var invitation = db.pm_projectTeam.Single(a => a.id == id);

                invitation.state = 1;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Msg = "Coudn't approve!";
                return View("Index");
            }
        }

        // POST: Invitation/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                pm_projectTeam invitation = db.pm_projectTeam.Find(id);
                db.pm_projectTeam.Remove(invitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Msg = "Error while deleting!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Invite(int id)
        {
            try
            {

                var invitation = db.pm_projectTeam.Single(a => a.id == id);

                invitation.state = 1;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Msg = "Coudn't approve!";
                return View("Index");
            }
        }
    }
}

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

            if (user_role_id == 5 || user_role_id == 1)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            var invitations = db.pm_projectTeam.Where(e => e.member_id == id).Where(e => e.state == 0).ToList();
            return View(invitations);
        }

        public ActionResult MyProjects()
        {
            int id = int.Parse(Session["user_id"].ToString());
            int user_role_id = int.Parse(Session["user_role_id"].ToString());

            if (user_role_id == 5 || user_role_id == 1)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            var invitations = db.pm_projectTeam.Where(e => e.member_id == id).Where(e => e.state == 1).ToList();
            return View(invitations);
        }

        public ActionResult ProjectDevelopers(int id)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            int user_role_id = int.Parse(Session["user_role_id"].ToString());

            if (user_role_id == 5 || user_role_id == 1)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            var invitations = db.pm_projectTeam.Where(e => e.project_id == id && e.postion == 4).Where(e => e.state == 1).ToList();
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

        public ActionResult InviteMembers(int id)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            var members = db.pm_User.Where( a => (a.role_id == 3 || a.role_id == 4) && a.id != user_id ).ToList();
            var members_clone = db.pm_User.Where( a => (a.role_id == 3 || a.role_id == 4) && a.id != user_id ).ToList();
            var invitations = db.pm_projectTeam.ToList();

            foreach(var member in members_clone)
            {
                foreach (var invitation in invitations)
                {
                    if (invitation.member_id == member.id && invitation.project_id == id)
                    {
                        members.Remove(member);
                    }
                }
            }
            ViewBag.project_id = id;
            return View(members);
        }

        [HttpPost]
        public ActionResult InviteMemberSave(pm_projectTeam invitation)
        {
            //pm_projectTeam invitation = new pm_projectTeam();
            ////return Content(project_id.ToString() + " " + member_id.ToString());
            //invitation.member_id = member_id;
            //invitation.project_id = project_id;
            invitation.postion = 4;
            invitation.state = 0;

            db.pm_projectTeam.Add(invitation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SoftDelete(int id)
        {
            var invitation = db.pm_projectTeam.Find(id);
            int project_id = int.Parse(invitation.project_id.ToString());
            if (invitation == null)
            {
                return Content("Invitation not found");
            }

            invitation.state = 3;
            db.SaveChanges();

            if (true)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            return View("Index");
        }

    }
}

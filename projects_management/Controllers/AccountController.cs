using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using projects_management.Models;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace projects_management_system.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: AccountUser

        //Return Registration View Page.
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        //Registration method to check and send data to DB
        [HttpPost]
        public ActionResult Register(pm_User account)

        {
            if (ModelState.IsValid)
            {
                var checkUsername = db.pm_User.Where(e => e.email == account.email).FirstOrDefault();
                var checkEmail = db.pm_User.Where(e => e.email == account.email).FirstOrDefault();
                if (checkUsername == null)
                {
                    if (checkEmail == null)
                    {
                        db.pm_User.Add(account);
                        db.SaveChanges();

                        ModelState.Clear();
                        ViewBag.Message = account.firstname + " " + account.lastname + " Successfully registered";
                    }
                    else
                    {
                        ModelState.AddModelError("", "This Email Address is already exists !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username is already exists, Check another one !");
                }
            }
            return View();
        }

        //Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(pm_User user)
        {
            
            var usr = db.pm_User.Where(u => u.email == user.email).Where(u => u.password == user.password).FirstOrDefault();

            if (usr != null)
            {
                Session["user_id"] = usr.id.ToString();
                Session["user_email"] = usr.email.ToString();
                Session["user_role_id"] = usr.role_id.ToString();
                Session["user_name"] = usr.firstname.ToString() + " " + usr.lastname.ToString();
                Session["user_photo"] = usr.photo.ToString();

                //get user role to switch the views of the actors
                int user_role = usr.role_id.Value;
                switch(user_role){
                    case 1:
                    {
                        //case 1 goto admin dashboard
                    return RedirectToAction("profile");
                    }
                    case 2:
                    {
                        //case 2 goto project customer dashboard
                        return RedirectToAction("profile");
                    }
                    case 3:
                    {
                            //case 2 goto project customer dashboard
                            return RedirectToAction("profile");
                    }
                    case 4:
                    {
                            //case 2 goto project customer dashboard
                            return RedirectToAction("profile");
                    }
                    case 5:
                    {
                        //case 2 goto project customer dashboard
                        return RedirectToAction("profile");
                    }     
                } 
            }
            else {
                ModelState.AddModelError("", "Invalid Username or password");
            }
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult profile()
        {
            if (Session["user_id"] != null)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                pm_User user = db.pm_User.Find(user_id);
                return View(user);
            }
            else
                return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult project_manager_content()
        {
            if (Session["user_id"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["user_email"] = null;
            Session["user_role_id"] = null;
            Session["user_name"] = null;
            Session["user_photo"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
       
    }
}
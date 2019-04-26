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

namespace projects_management_system.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: AccountUser

        //Return Registration View Page.
        public ActionResult Register()
        {
            return View();
        }

        //Registration method to check and send data to DB
        [HttpPost]
        public ActionResult Register(pm_User account)

        {
            if (ModelState.IsValid)
            {
                projects_management_systemEntities db = new projects_management_systemEntities();

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
            using (projects_management_systemEntities db = new projects_management_systemEntities())
            {
                var usr = db.pm_User.Where(u => u.email == user.email && u.password == user.password).FirstOrDefault();
                if (usr != null)
                {
                     Session["user_id"] = usr.id.ToString();
                    Session["user_email"] = usr.email.ToString();
                    //get user role to switch the views of the actors
                    int user_role = usr.role_id.Value;
                    switch(user_role){
                        case 1:
                            {
                             //case 1 goto admin dashboard
                            return RedirectToAction("LoggedIn");
                            }
                            case 2:
                            {
                                //case 2 goto project manager dashboard
                                return RedirectToAction("project_manager_content");
                            }     
                    } 
                }
                else {
                    ModelState.AddModelError("", "Invalid Username or password");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoggedIn()
        {
            if (Session["user_id"] != null)
                return View();
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


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        

       
    }
}
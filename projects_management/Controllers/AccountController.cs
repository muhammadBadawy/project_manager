﻿using System;
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
        // GET: AccountUser

        //Return Registration View Page.
        [AllowAnonymous]
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
                DBEntities db = new DBEntities();

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
            using (DBEntities db = new DBEntities())
            {
                var usr = db.pm_User.Where(u => u.email == user.email).Where(u => u.password == user.password).FirstOrDefault();

                if (usr != null)
                {
                    Session["user_id"] = usr.id.ToString();
                    Session["user_email"] = usr.email.ToString();
                    Session["user_role_id"] = usr.role_id.ToString();

                    //get user role to switch the views of the actors
                    int user_role = usr.role_id.Value;
                    switch(user_role){
                        case 1:
                            {
                             //case 1 goto admin dashboard
                            return RedirectToAction("LoggedIn");
                            }
                            case 5:
                            {
                                //case 2 goto project customer dashboard
                               return RedirectToAction("LoggedIn");
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
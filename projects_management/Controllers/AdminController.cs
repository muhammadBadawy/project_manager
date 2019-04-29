﻿using projects_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace projects_management.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        projects_management_systemEntities db = new projects_management_systemEntities();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        [HttpGet]
        public ActionResult AddUser()
        {
            pm_User userModel = new pm_User();
            ViewBag.roles = new SelectList(db.pm_Role, "id", "role_title");

            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddUser(pm_User userModel)
        {

            if (!ModelState.IsValid)
            {
                return Content("Not valid bro?!");
            }

            db.pm_User.Add(userModel);
            db.SaveChanges();

            ViewBag.roles = new SelectList(db.pm_Role, "id", "role_title");
            //return View("AllUsers");
            return RedirectToAction("AllUsers");
        }


        [HttpGet]

        public ActionResult EditUser(int? id)
        {
            var usr = db.pm_User.Find(id);
            if (usr == null)
            {
                return Content("User not found");
            }
            return View("EditUser", usr);

        }


        [HttpPost]

        public ActionResult EditUser(pm_User editUsers)
        {

            if (!ModelState.IsValid)
            {
                return View("EditUser", editUsers);
            }

            var UserDB = db.pm_User.Single(a => a.id == editUsers.id);
            UserDB.firstname = editUsers.firstname;
            UserDB.lastname = editUsers.lastname;
            UserDB.email = editUsers.email;
            UserDB.password = editUsers.password;
            UserDB.mobile = editUsers.mobile;
            UserDB.photo = editUsers.photo;

            db.SaveChanges();
            return View("EditUser", UserDB);   //***** page that are U redirect to it

        }

        /////delet user from db
        [HttpGet, ActionName("AllUsers")]
        public ActionResult AllUsers()
        {
            //pm_User users = db.pm_User.ToList();

            //if (users == null)
            //{
            //    return HttpNotFound();
            //}
            return View(db.pm_User.ToList());

        }

        [HttpGet]
        public ActionResult UserDetails(int? id)
        {
            //pm_User users = db.pm_User.ToList();

            //if (users == null)
            //{
            //    return HttpNotFound();
            //}
            return View(db.pm_User.Find(id));

        }

        // POST:
        [HttpGet, ActionName("UserDelete")]
        
        public ActionResult UserDelete(int? id)
        {
            pm_User user = db.pm_User.Find(id);
            db.pm_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("AllUsers");
        }
        

    }
}
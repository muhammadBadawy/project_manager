using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using customerApp.Models;
using System.Data.Entity;

namespace customerApp.Controllers
{
    [Authorize(Roles = "Admins")]          ////// Admins only can makeing ...
    public class AdminController : Controller
    {

        private DbModels db = new DbModels();


        // GET: Admin
        [HttpGet]
        public ActionResult AddUser()
        {
            pm_User userModel = new pm_User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddUser(pm_User userModel)
        {

            if (!ModelState.IsValid)
            {
                return View("AdminProfile", userModel);
            }

            db.pm_User.Add(userModel);
            db.SaveChanges();

            return View("AdminProfile");
        }




        [HttpGet]

        public ActionResult EditUser(int? id)
        {


            var poject = db.pm_User.Single(a => a.id == id);


            if (id == null)
            {
                return HttpNotFound();

            }

            var projectid = db.pm_User.Find();
            if (projectid == null)
            {
                return HttpNotFound();
            }



            return RedirectToAction("AdminProfile");/////////////************************

        }


        [HttpPost]

        public ActionResult EditUser(pm_User editUsers)
        {

            if (!ModelState.IsValid)
            {
                return View("AdminProfile", editUsers);
            }

            var UserDB = db.pm_User.Single(a => a.id == editUsers.id);
            UserDB.firstname = editUsers.firstname;
            UserDB.lastname = editUsers.lastname;
            UserDB.email = editUsers.email;
            UserDB.password = editUsers.password;
            UserDB.mobile = editUsers.mobile;
            UserDB.photo = editUsers.photo;

            db.SaveChanges();
            return RedirectToAction("AdminProfile");   //***** page that are U redirect to it

        }






        /////delet user from db
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            pm_User users = db.pm_User.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }


            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);

        }

        // POST:
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(pm_User users)
        {


            pm_User posts = db.pm_User.Find(users.id);
            db.pm_User.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("AdminProfile");


        }




        ////////////////////////////////////////////////// ****************Admin control posts ***************////////////////////////////////////
        ///  Control the Home Profile: Add, Remove, Update Posts (Means Projects) created by the customers.
        /// Add post
        /// Remove post
        /// Update post




        //////////////////////////////////////////////********* get and approve posts **********///////////////////////




        [HttpGet]
        public ActionResult GetPosts()
        {
            pm_project ApprovedPost = new pm_project();
            if (ApprovedPost.p_state == 0)
            {
                return HttpNotFound();
            }

            var NotApprove = new SelectList(db.pm_project.ToList());
            ViewBag.NotApprovedPosts = NotApprove;

            return View(NotApprove);


        }


        [HttpGet]
        public ActionResult GetUsers()
        {


            var AllUsers = new SelectList(db.pm_User.ToList());
            ViewBag.UsersDB = AllUsers;

            return View(AllUsers);

            ///****OR ***////

            //public ActionResult GetUsers()
            // {
            //    var user = db.pm_User;
            //   return View(user);
            // }




        }







        [HttpGet]
        public ActionResult ApprovePost(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            pm_project post = db.pm_project.Find(id);
            //int? sta = post.p_state;

            if (post == null && post.p_state == null)
            {
                return HttpNotFound();
            }
            return View(post);

        }
        [HttpPost]
        public ActionResult ApprovePost(pm_project post)
        {
            pm_project sta = new pm_project();
            if (sta.p_state != 1)
            {
                return ViewBag.SuccessMessage = " can not approve post ,it is approved befor ";
            }
            var pojectDB = db.pm_project.Single(a => a.id == post.id);
            pojectDB.p_state = 0;
            db.SaveChanges();
            return RedirectToAction("AdminProfile");

        }




        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            pm_project post = db.pm_project.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }


            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);

        }

        // POST:
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(pm_project post)
        {


            pm_project posts = db.pm_project.Find(post.id);
            db.pm_project.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("AdminProfile");


        }


        [HttpGet]
        public ActionResult EditPost(int id)
        {
            var post = db.pm_project.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [HttpPost]
        public ActionResult Edit(pm_project post)
        {
            if (ModelState.IsValid)
            {

                post.start_date = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminProfile");
            }
            return View();
        }



        public ActionResult DetailsPost(int projectID)
        {
            var post = db.pm_project.Find(projectID);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);

        }

        public ActionResult Search()
        {
            return View();


        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {

            var result = db.pm_Role.Where(a => a.role_title.Contains(searchName)).ToList();

            return View(result);

        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using customerApp.Models;

namespace customerApp.Controllers
{
    public class UserController : Controller

    {


        [HttpGet]
        public ActionResult CustomerRegister()
        {
            pm_User userModel = new pm_User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult CustomerRegister(pm_User userModel)
        {

            using (DbModels db = new DbModels())
            {
                if (!ModelState.IsValid)
                {
                    return View("CustomerRegister", userModel);
                }

                db.pm_User.Add(userModel);
                db.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMessage = " done ";
            return RedirectToAction("CustomerRegister"); //// return to login page  *CustomerRegister* just fo test

        }


        //customer_aCreating projects.
        [HttpGet]
        public ActionResult CreateProject()
        {
            pm_project AddPost = new pm_project();

            return View(AddPost);
        }

        [HttpPost]
        public ActionResult CreateProject(pm_project AddPost)
        {
            pm_project role = new pm_project();
            using (DbModels db = new DbModels())
            {
                if (!ModelState.IsValid)
                {
                    return View("CreateProject", AddPost);
                }

                db.pm_project.Add(AddPost);
                db.SaveChanges();


                ViewBag.project_manger_id = new SelectList(db.pm_User, "id", "Email", role.project_manger_id);

                ModelState.Clear();
                ViewBag.SuccessMessage = " post added!  ";


                return View("AddPost");            //return to customer profile 

            }
        }

        [HttpGet]
        public ActionResult EditProject(int? id)
        {
            using (DbModels db = new DbModels())
            {

                var poject = db.pm_project.Single(a => a.id == id);


                if (id == null)
                {
                    return HttpNotFound();

                }

                pm_project stat = new pm_project();
                if (stat.p_state == 0)
                {
                    return HttpNotFound();
                }



                return View(poject);/////////////************************
            }
        }

        [HttpPost]
        public ActionResult EditProject(pm_project editpost)
        {
            using (DbModels db = new DbModels())
            {
                if (!ModelState.IsValid)
                {
                    return View("EditProject", editpost);
                }

                var pojectDB = db.pm_project.Single(a => a.id == editpost.id);
                pojectDB.title = editpost.title;
                pojectDB.p_description = editpost.p_description;
                pojectDB.price = editpost.price;
                pojectDB.start_date = editpost.start_date;
                pojectDB.end_date = editpost.end_date;

                db.SaveChanges();
                return RedirectToAction("HomePage");   //***** page that are U redirect to it 
            }
        }




        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (DbModels db = new DbModels())
            {

                if (id == null)
                {
                    return HttpNotFound();
                }
                pm_project stat = new pm_project();
                if (stat.p_state == 0)
                {
                    return HttpNotFound();
                }
                return View(stat);
            }
        }

        // POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(pm_project stat)
        {
            using (DbModels db = new DbModels())
            {

                pm_project posts = db.pm_project.Find(stat.id);
                db.pm_project.Remove(posts);
                db.SaveChanges();
                return RedirectToAction("HomePage");
            }

        }



        [HttpGet]
        public ActionResult GetPosts()
        {
            using (DbModels db = new DbModels())
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
        }





    }


}
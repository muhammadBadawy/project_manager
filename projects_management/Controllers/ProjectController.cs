using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projects_management.Models;

namespace projects_management_system.Controllers
{
    public class ProjectController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Project
        public ActionResult Index()
        {
            int user_role_id = int.Parse(Session["user_role_id"].ToString());
            if (user_role_id == 1) //if admin get all projects
            {
               
                return View(db.pm_project.ToList());
            }
            else if (user_role_id == 5)//if customer get customer projects
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                
                return View(db.pm_project.Where(p => p.customer_id == user_id));
            }
            else
            {
                return View();
            }
          
       
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            var project = db.pm_project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
         
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(pm_project project)
        { 
            try
            {
                // TODO: Add insert logic here
                //save the id of the user to table
                project.customer_id =  int.Parse(Session["user_id"].ToString());
                project.admin_approved = 0;
                project.p_state = 0;

                db.pm_project.Add(project);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Msg = "Project Added successfully!";
                return View();
            }
            catch
            {
                ViewBag.Msg = "Can't add project";
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            var project = db.pm_project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(pm_project editedProject)
        {
            try
            {
                // TODO: Add update logic here
                  if (editedProject != null) {
                     var project =  db.pm_project.Find(editedProject.id);
              
                    project.title = editedProject.title;
                    project.p_description = editedProject.p_description;
                    project.price = editedProject.price;
                    project.start_date = editedProject.start_date;
                    project.end_date = editedProject.end_date;
                
                    db.SaveChanges();
                }
               
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            pm_project project = db.pm_project.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }


            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(pm_project projectDeleted)
        {
            try
            {
                // TODO: Add delete logic here
                var project = db.pm_project.Find(projectDeleted.id);
                db.pm_project.Remove(project);
                db.SaveChanges();
            
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

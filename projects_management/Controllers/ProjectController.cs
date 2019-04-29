﻿using System;
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

                return View(db.pm_project.Where(p => p.admin_approved == 1 && p.customer_id == user_id));
            }
            else if (user_role_id == 2)//if Project manager get Project manager approved projects
            {
                //Get all projects approved by admin
                return View(db.pm_project.Where(p => p.admin_approved == 1 && p.project_manger_id == null));
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
                project.customer_id = int.Parse(Session["user_id"].ToString());
                project.admin_approved = 0;
                project.p_state = 1;

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
        // GET: Project/Delivered
        public ActionResult Delivered()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            return View(db.pm_project.Where(p => p.customer_id == user_id && p.p_state == 0));
        }
        // GET: Project/Notdelivered
        public ActionResult Notdelivered()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            return View(db.pm_project.Where(p => p.customer_id == user_id && p.p_state == 1));
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
                if (editedProject != null)
                {
                    var project = db.pm_project.Find(editedProject.id);

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



        // GET: Project/Requests/5
        public ActionResult Requests(int id)
        {
            //postition
            //2-PM
            //3-TEAM LEADER
            //4-DEV
            var project_requests = db.pm_projectTeam.Where(team => team.project_id == id && team.postion == 2);
            if (project_requests == null)
            {
                return HttpNotFound();
            }
            return View(project_requests);
        }

        // GET: Project/Approve/5
        public ActionResult Approve(int id)
        {

            pm_projectTeam team_member_request = db.pm_projectTeam.Find(id);
            //project id
            int project_id = int.Parse(team_member_request.project_id.ToString());
            //project manager id
            int project_manager_id = int.Parse(team_member_request.member_id.ToString());
            //update state
            team_member_request.state = 1;
            //delete other requests
            var requests = db.pm_projectTeam.Where(t => t.id != id);
            //remove other requests
            db.pm_projectTeam.RemoveRange(requests);
            //get project to change project manager id 
            var approved_project = db.pm_project.Find(project_id);
            //update project manger id in project table
            approved_project.project_manger_id = project_manager_id;
            db.SaveChanges();

            if (team_member_request == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }


        // GET: Project/Current/
        public ActionResult Current()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            return View(db.pm_project.Where(p => p.admin_approved == 1 && p.project_manger_id == user_id && p.p_state == 1));
        }
        // GET: Project/History
        public ActionResult History()
        {
            int projectmanager_id = int.Parse(Session["user_id"].ToString());
            //Get finsished projects
            return View(db.pm_project.Where(p => p.admin_approved == 1 && p.project_manger_id == projectmanager_id && p.p_state == 0));
        }



        // GET: Project/Leave/5
        public ActionResult Leave(int id)
        {
            int project_manager_id = int.Parse(Session["user_id"].ToString());
            pm_project project = db.pm_project.Find(id);
            project.project_manger_id = null;
            db.SaveChanges();
            //delete the project manager from team
            var project_team_req = db.pm_projectTeam.Where(req => req.project_id == id && req.member_id == project_manager_id);
            db.pm_projectTeam.RemoveRange(project_team_req);

            if (project == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Current");
        }

        // GET: Project/Apply/5
        public ActionResult Apply(int id)
        {
            int project_manager_id = int.Parse(Session["user_id"].ToString());
            var req_found = db.pm_projectTeam.Where(team => team.project_id == id && team.member_id == project_manager_id);
       
            
            if (req_found == null)
            {
                ViewBag.Msg = "You have sent request to joing this project, your request is still pending!";
                 return RedirectToAction("Index");
            }
            else
            {
            pm_projectTeam req = new pm_projectTeam();
            req.member_id = project_manager_id;
            req.project_id = id;
            req.postion = 2;
            req.state = 0;

            db.pm_projectTeam.Add(req);
            db.SaveChanges();
            ViewBag.Msg = "Request Sent successfully !";
            return RedirectToAction("Index");
            }

            
        }
    }
}

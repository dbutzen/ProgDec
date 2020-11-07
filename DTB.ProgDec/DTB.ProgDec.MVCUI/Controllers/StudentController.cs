using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class StudentController : Controller
    {
        List<Student> students;
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            students = StudentManager.Load();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Details";
            Student student = StudentManager.LoadById(id);
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            Student student = new Student();
            return View(student);
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                StudentManager.Insert(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
            Student student = StudentManager.LoadById(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                // TODO: Add update logic here
                StudentManager.Update(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete";
            Student student = StudentManager.LoadById(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Student student)
        {
            try
            {
                // TODO: Add delete logic here
                StudentManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

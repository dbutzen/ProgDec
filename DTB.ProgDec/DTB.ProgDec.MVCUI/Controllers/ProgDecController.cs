using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.MVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class ProgDecController : Controller
    {
        // GET: ProgDec
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            var progdecs = ProgDecManager.Load();
            return View(progdecs);
        }

        public ActionResult Load(int id)
        {
            var progdecs = ProgDecManager.Load(id);
            return View("Index", progdecs);
        }

        // GET: ProgDec/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Details";
            var progdec = ProgramManager.LoadById(id);
            return View(progdec);
        }

        // GET: ProgDec/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ProgDecProgramsStudents pps = new ProgDecProgramsStudents();
            pps.ProgDec = new BL.Models.ProgDec();
            pps.Programs = ProgramManager.Load();
            pps.Students = StudentManager.Load();


            return View(pps);
        }

        // POST: ProgDec/Create
        [HttpPost]
        public ActionResult Create(ProgDecProgramsStudents pps)
        {
            try
            {
                // TODO: Add insert logic here
                ProgDecManager.Insert(pps.ProgDec);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgDec/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
            ProgDecProgramsStudents pps = new ProgDecProgramsStudents();


            pps.ProgDec = ProgDecManager.LoadById(id);
            pps.Programs = ProgramManager.Load();
            pps.Students = StudentManager.Load();


            return View(pps);
        }

        // POST: ProgDec/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProgDecProgramsStudents pps)
        {
            try
            {
                // TODO: Add update logic here
                ProgDecManager.Update(pps.ProgDec);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgDec/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete";
            BL.Models.ProgDec progdec = ProgDecManager.LoadById(id);
            return View(progdec);
        }

        // POST: ProgDec/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ProgDecManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

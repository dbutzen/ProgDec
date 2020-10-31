using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class DegreeTypeController : Controller
    {
        List<DegreeType> degreeTypes;
        // GET: DegreeType
        public ActionResult Index()
        {
            degreeTypes = DegreeTypeManager.Load();
            return View(degreeTypes);
        }

        // GET: DegreeType/Details/5
        public ActionResult Details(int id)
        {
            DegreeType degreeType = new DegreeType();
            degreeType = DegreeTypeManager.LoadById(id);
            return View();
        }

        // GET: DegreeType/Create
        public ActionResult Create()
        {
            DegreeType degreeType = new DegreeType();
            return View(degreeType);
        }

        // POST: DegreeType/Create
        [HttpPost]
        public ActionResult Create(DegreeType degreeType)
        {
            try
            {
                // TODO: Add insert logic here
                DegreeTypeManager.Insert(degreeType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeType/Edit/5
        public ActionResult Edit(int id)
        {
            DegreeType degreeType = new DegreeType();
            degreeType = DegreeTypeManager.LoadById(id);
            return View();
        }

        // POST: DegreeType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DegreeType degreeType)
        {
            try
            {
                // TODO: Add update logic here
                DegreeTypeManager.Update(degreeType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeType/Delete/5
        public ActionResult Delete(int id)
        {
            DegreeType degreeType = new DegreeType();
            degreeType = DegreeTypeManager.LoadById(id);
            return View();
        }

        // POST: DegreeType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DegreeType degreeType)
        {
            try
            {
                // TODO: Add delete logic here
                DegreeTypeManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

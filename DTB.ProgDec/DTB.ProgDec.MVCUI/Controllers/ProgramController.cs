using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.BL;
using DTB.ProgDec.MVCUI.ViewModels;
using System.IO;
using DTB.ProgDec.MVCUI.Models;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            if (Authenticate.IsAuthenticated())
            {
                var programs = ProgramManager.Load();

                return View(programs);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            var programs = ProgramManager.Load();
            return PartialView(programs);
        }

        // GET: Program/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Details";
            Program program = ProgramManager.LoadById(id);
            return View(program);
        }

        // GET: Program/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            if (Authenticate.IsAuthenticated())
            {
                ProgramDegreeTypes pdts = new ProgramDegreeTypes();

                pdts.DegreeTypes = DegreeTypeManager.Load();
                pdts.Program = new Program();

                return View(pdts);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Program/Create
        [HttpPost]
        public ActionResult Create(ProgramDegreeTypes pdts)
        {
            try
            {
                if(pdts.File != null)
                {
                    pdts.Program.ImagePath = pdts.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdts.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        pdts.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "File already exists...";
                    }
                }

                // TODO: Add insert logic here
                ProgramManager.Insert(pdts.Program);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pdts);
            }
        }

        // GET: Program/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
            if (Authenticate.IsAuthenticated()) { 
            ProgramDegreeTypes pdts = new ProgramDegreeTypes();
            pdts.DegreeTypes = DegreeTypeManager.Load();
            pdts.Program = ProgramManager.LoadById(id);

            return View(pdts);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Program/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProgramDegreeTypes pdts)
        {
            try
            {
                if (pdts.File != null)
                {
                    pdts.Program.ImagePath = pdts.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(pdts.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        pdts.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "File already exists...";
                    }
                }
                // TODO: Add update logic here
                ProgramManager.Update(pdts.Program);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pdts);
            }
        }

        // GET: Program/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete";
            if (Authenticate.IsAuthenticated()) { 
            Program program = ProgramManager.LoadById(id);
            return View(program);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Program/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Program program)
        {
            try
            {
                // TODO: Add delete logic here
                ProgramManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

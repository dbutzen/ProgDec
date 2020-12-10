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
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class ProgramController : Controller
    {
        #region "Pre-WebAPI"

        // GET: Program
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            if (Authenticate.IsAuthenticated())
            {
                var programs = ProgramManager.Load();
                ViewBag.Source = "Index";
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

        #endregion

        #region "WebAPI"

        private static HttpClient InitializationClient()
        {
            HttpClient client = new HttpClient();
        //client.BaseAddress = new Uri("https://localhost:44317/api/");
        
            client.BaseAddress = new Uri("http://dtbprogdecapi.azurewebsites.net/api/");
            return client;
        }

        public ActionResult Get()
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("Program").Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //Pase the items into a list of program
            List<Program> programs = items.ToObject<List<Program>>();

            ViewBag.Source = "Get";
            return View("Index", programs);

        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("Program/" + id).Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            Program program = JsonConvert.DeserializeObject<Program>(result);

            return View("Details", program);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializationClient();

            ProgramDegreeTypes pdts = GetDegreeTpyes(client);

            pdts.Program = new Program();
            return View("Create", pdts);
        }
        [HttpPost]
        public ActionResult Insert(ProgramDegreeTypes pdts)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PostAsJsonAsync("Program", pdts.Program).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", pdts);
            }
            
        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializationClient();

            ProgramDegreeTypes pdts = GetDegreeTpyes(client);

            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            pdts.Program = JsonConvert.DeserializeObject<Program>(result);

            return View("Edit", pdts);
        }

        private static ProgramDegreeTypes GetDegreeTpyes(HttpClient client)
        {
            ProgramDegreeTypes pdts = new ProgramDegreeTypes();
            pdts.DegreeTypes = DegreeTypeManager.Load();
            HttpResponseMessage response = client.GetAsync("DegreeType").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            pdts.DegreeTypes = items.ToObject<List<DegreeType>>();
            return pdts;
        }

        [HttpPost]
        public ActionResult Update(int id, ProgramDegreeTypes pdts)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PutAsJsonAsync("Program/" + id, pdts.Program).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", pdts);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializationClient();
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Program program = JsonConvert.DeserializeObject<Program>(result);
            return View("Delete", program);
        }

        [HttpPost]
        public ActionResult Remove(int id, Program program)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.DeleteAsync("Program/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", program);
            }
            
        }

        #endregion
    }
}

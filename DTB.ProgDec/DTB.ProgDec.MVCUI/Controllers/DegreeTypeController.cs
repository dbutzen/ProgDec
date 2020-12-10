using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DTB.ProgDec.MVCUI.Controllers
{
    
    public class DegreeTypeController : Controller
    {
        #region "Pre-WebAPI"
        List<DegreeType> degreeTypes;
        // GET: DegreeType
        public ActionResult Index()
        {
            ViewBag.Title = "Degree Types";
            degreeTypes = DegreeTypeManager.Load();
            return View(degreeTypes);
        }

        // GET: DegreeType/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Edit";
            DegreeType degreeType = new DegreeType();
            degreeType = DegreeTypeManager.LoadById(id);
            return View();
        }

        // GET: DegreeType/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
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
            ViewBag.Title = "Edit";

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
            ViewBag.Title = "Delete";

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
            HttpResponseMessage reponse = client.GetAsync("DegreeType").Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //Pase the items into a list of degreeType
            List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();

            ViewBag.Source = "Get";
            return View("Index", degreeTypes);

        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("DegreeType/" + id).Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            DegreeType degreeType = JsonConvert.DeserializeObject<DegreeType>(result);

            return View("Details", degreeType);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializationClient();

            DegreeType degreeType = new DegreeType();
            return View("Create", degreeType);
        }
        [HttpPost]
        public ActionResult Insert(DegreeType degreeType)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PostAsJsonAsync("DegreeType", degreeType).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", degreeType);
            }

        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializationClient();


            HttpResponseMessage response = client.GetAsync("DegreeType/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            DegreeType degreeType = JsonConvert.DeserializeObject<DegreeType>(result);

            return View("Edit", degreeType);
        }



        [HttpPost]
        public ActionResult Update(int id, DegreeType degreeType)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PutAsJsonAsync("DegreeType/" + id, degreeType).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", degreeType);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializationClient();
            HttpResponseMessage response = client.GetAsync("DegreeType/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            DegreeType degreeType = JsonConvert.DeserializeObject<DegreeType>(result);
            return View("Delete", degreeType);
        }

        [HttpPost]
        public ActionResult Remove(int id, DegreeType degreeType)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.DeleteAsync("DegreeType/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", degreeType);
            }

        }

        #endregion
    }
}

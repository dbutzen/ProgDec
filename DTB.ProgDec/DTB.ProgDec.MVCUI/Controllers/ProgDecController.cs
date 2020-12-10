using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.MVCUI.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class ProgDecController : Controller
    {
        #region "Pre-WebAPI"
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
            var progdec = ProgDecManager.LoadById(id);
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

            pps.Advisors = AdvisorManager.Load(); //Load All


            return View(pps);
        }

        // POST: ProgDec/Create
        [HttpPost]
        public ActionResult Create(ProgDecProgramsStudents pps)
        {
            try
            {
                ProgDecManager.Insert(pps.ProgDec);
                pps.AdvisorIds.ToList().ForEach(a => ProgDecAdvisorManager.Add(pps.ProgDec.Id, a));
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
            pps.Advisors = AdvisorManager.Load(); //Load All
            pps.ProgDec.Advisors = ProgDecManager.LoadAdvisors(id);
            pps.AdvisorIds = pps.ProgDec.Advisors.Select(a => a.Id); // Select the ids


            // Put existing advisors for this progdec in session.
            Session["advisorids"] = pps.AdvisorIds;
            return View(pps);
        }

        // POST: ProgDec/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProgDecProgramsStudents pps)
        {
            try
            {
                //Deal with the advisors
                IEnumerable<int> oldadvisorids = new List<int>();
                if(Session["advisorids"] != null)
                {
                    oldadvisorids = (IEnumerable<int>)Session["advisorids"];
                }

                IEnumerable<int> newadvisorids = new List<int>();
                if(pps.AdvisorIds != null)
                {
                    newadvisorids = pps.AdvisorIds;
                }

                // Identify deletes
                IEnumerable<int> deletes = oldadvisorids.Except(newadvisorids);

                // Identify Adds
                IEnumerable<int> adds = newadvisorids.Except(oldadvisorids);

                deletes.ToList().ForEach(d => ProgDecAdvisorManager.Delete(id, d));
                adds.ToList().ForEach(a => ProgDecAdvisorManager.Add(id, a));

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
        public ActionResult Delete(int id, BL.Models.ProgDec progDec)
        {
            try
            {
                // TODO: Add delete logic here
                progDec.Advisors = ProgDecManager.LoadAdvisors(id);
                progDec.Advisors.ForEach(a => ProgDecAdvisorManager.Delete(id, a.Id));

                ProgDecManager.Delete(id);
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
            HttpResponseMessage reponse = client.GetAsync("ProgDec").Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //Pase the items into a list of progDec
            List<BL.Models.ProgDec> progDecs = items.ToObject<List<BL.Models.ProgDec>>();

            ViewBag.Source = "Get";
            return View("Index", progDecs);

        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("ProgDec/" + id).Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            BL.Models.ProgDec progDec = JsonConvert.DeserializeObject<BL.Models.ProgDec>(result);

            return View("Details", progDec);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializationClient();

            BL.Models.ProgDec progDec = new BL.Models.ProgDec();
            return View("Create", progDec);
        }
        [HttpPost]
        public ActionResult Insert(BL.Models.ProgDec progDec)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PostAsJsonAsync("ProgDec", progDec).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", progDec);
            }

        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializationClient();


            HttpResponseMessage response = client.GetAsync("ProgDec/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            BL.Models.ProgDec progDec = JsonConvert.DeserializeObject<BL.Models.ProgDec>(result);

            return View("Edit", progDec);
        }



        [HttpPost]
        public ActionResult Update(int id, BL.Models.ProgDec progDec)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PutAsJsonAsync("ProgDec/" + id, progDec).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", progDec);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializationClient();
            HttpResponseMessage response = client.GetAsync("ProgDec/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            BL.Models.ProgDec progDec = JsonConvert.DeserializeObject<BL.Models.ProgDec>(result);
            return View("Delete", progDec);
        }

        [HttpPost]
        public ActionResult Remove(int id, BL.Models.ProgDec progDec)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.DeleteAsync("ProgDec/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", progDec);
            }

        }

        #endregion
    }
}

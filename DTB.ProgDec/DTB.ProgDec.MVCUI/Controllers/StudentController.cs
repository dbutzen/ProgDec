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
    public class StudentController : Controller
    {
        List<Student> students;

        #region "Pre-WebAPI"
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
            HttpResponseMessage reponse = client.GetAsync("Student").Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //Pase the items into a list of student
            List<Student> students = items.ToObject<List<Student>>();

            ViewBag.Source = "Get";
            return View("Index", students);

        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("Student/" + id).Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            Student student = JsonConvert.DeserializeObject<Student>(result);

            return View("Details", student);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializationClient();

            Student student = new Student();
            return View("Create", student);
        }
        [HttpPost]
        public ActionResult Insert(Student student)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PostAsJsonAsync("Student", student).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", student);
            }

        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializationClient();

            
            HttpResponseMessage response = client.GetAsync("Student/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Student student = JsonConvert.DeserializeObject<Student>(result);

            return View("Edit", student);
        }

        

        [HttpPost]
        public ActionResult Update(int id, Student student)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PutAsJsonAsync("Student/" + id, student).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", student);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializationClient();
            HttpResponseMessage response = client.GetAsync("Student/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Student student = JsonConvert.DeserializeObject<Student>(result);
            return View("Delete", student);
        }

        [HttpPost]
        public ActionResult Remove(int id, Student student)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.DeleteAsync("Student/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", student);
            }

        }

        #endregion
    }
}

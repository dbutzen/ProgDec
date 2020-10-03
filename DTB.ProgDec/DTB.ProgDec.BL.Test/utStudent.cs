using System;
using System.Collections.Generic;
using DTB.ProgDec.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTB.ProgDec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void Load()
        {
            List<Student> students = new List<Student>();
            students = StudentManager.Load();
            int expected = 4;
            Assert.AreEqual(expected, students.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            Student student = new Student();
            student.FirstName = "Test";
            student.LastName = "Student";
            student.StudentId = "7";

            int result = StudentManager.Insert(student, true);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Student student = StudentManager.LoadById(3);
            student.FirstName = "Test";
            student.LastName = "Student";

            int result = StudentManager.Update(student, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = StudentManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
    }
}

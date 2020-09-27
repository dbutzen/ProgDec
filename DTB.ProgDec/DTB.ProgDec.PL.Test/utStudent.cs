using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.PL;
using System.Linq;
using System.Data.Entity;

namespace DTB.ProgDec.PL.Test
{
    [TestClass]
    public class utStudent
    {
        protected ProgDecEntities dc;
        protected DbContextTransaction transaction;
        [TestInitialize]
        public void Initialize()
        {
            dc = new ProgDecEntities();
            transaction = dc.Database.BeginTransaction();
        }
        [TestCleanup]
        public void TransactionCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }
        [TestMethod]
        public void LoadStudentsTest()
        {


            int expected = 4;
            int actual = 0;

            var students = dc.tblStudents;

            actual = students.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadStudentsLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblStudent
            var students = from dt in dc.tblStudents
                              select dt;

            actual = students.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);
            dc = null;

        }

        [TestMethod]
        public void InsertTest()
        {

            // dc only exists in here
            // type = 1 row, types = all rows

            //make new row
            tblStudent newrow = new tblStudent();

            //set column values
            newrow.Id = -99;
            newrow.FirstName = "Goofy";
            newrow.LastName = "The Dog";
            newrow.StudentId = "123456789";

            // Insert of the row
            dc.tblStudents.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }

        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblStudent where id = -99
            tblStudent existingStudent = (from dt in dc.tblStudents
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingStudent != null)
            {
                //update description
                existingStudent.FirstName = "goofy";
                existingStudent.LastName = "Dog";
                existingStudent.StudentId = "123555789";
                dc.SaveChanges();
            }

            tblStudent updatedStudent = (from dt in dc.tblStudents
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingStudent.FirstName, updatedStudent.FirstName);

        }

        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblStudent where id = -99
            tblStudent existingStudent = (from dt in dc.tblStudents
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingStudent != null)
            {
                //update description
                dc.tblStudents.Remove(existingStudent);
                dc.SaveChanges();
            }

            tblStudent deletedStudent = (from dt in dc.tblStudents
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedStudent);

        }
    }
}

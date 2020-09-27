using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.PL;
using System.Linq;
using System.Data.Entity;

namespace DTB.ProgDec.PL.Test
{
    [TestClass]
    public class utProgDec
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
        public void LoadProgDecsTest()
        {


            int expected = 3;
            int actual = 0;

            var progDecs = dc.tblProgDecs;

            actual = progDecs.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadProgDecsLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 3;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblProgDec
            var progDecs = from dt in dc.tblProgDecs
                              select dt;

            actual = progDecs.Count();

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
            tblProgDec newrow = new tblProgDec();

            //set column values
            newrow.Id = -99;
            newrow.ProgramId = 5;
            newrow.StudentId = 3;
            newrow.ChangeDate = DateTime.Now;

            // Insert of the row
            dc.tblProgDecs.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }

        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblProgDec where id = -99
            tblProgDec existingProgDec = (from dt in dc.tblProgDecs
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingProgDec != null)
            {
                //update description

                existingProgDec.ProgramId = 6;
                existingProgDec.StudentId = 2;
                existingProgDec.ChangeDate = DateTime.Now;
                dc.SaveChanges();
            }

            tblProgDec updatedProgDec = (from dt in dc.tblProgDecs
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingProgDec.ProgramId, updatedProgDec.ProgramId);

        }

        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblProgDec where id = -99
            tblProgDec existingProgDec = (from dt in dc.tblProgDecs
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingProgDec != null)
            {
                //update description
                dc.tblProgDecs.Remove(existingProgDec);
                dc.SaveChanges();
            }

            tblProgDec deletedProgDec = (from dt in dc.tblProgDecs
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedProgDec);

        }
    }
}

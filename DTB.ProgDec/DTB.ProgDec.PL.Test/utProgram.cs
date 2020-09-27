using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.PL;
using System.Linq;
using System.Data.Entity;

namespace DTB.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
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
        public void LoadTest()
        {

            int expected = 16;
            int actual = 0;

            var programs = dc.tblPrograms;

            actual = programs.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadLINQTest()
        {


            int expected = 16;
            int actual = 0;

            var programs = from dt in dc.tblPrograms
                              select dt;

            actual = programs.Count();

            Assert.AreEqual(expected, actual);
            dc = null;

        }

        [TestMethod]
        public void InsertTest()
        {
           
            tblProgram newrow = new tblProgram();

            newrow.Id = -99;
            newrow.Description = "My New Program";

            dc.tblPrograms.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);


            
        }

        [TestMethod]
        public void UpdateTest()
        {
            
            tblProgram existingProgram = (from dt in dc.tblPrograms
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingProgram != null)
            {
                existingProgram.Description = "Test";
                dc.SaveChanges();
            }

            tblProgram updatedProgram = (from dt in dc.tblPrograms
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            Assert.AreEqual(existingProgram.Description, updatedProgram.Description);
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            
            tblProgram existingProgram = (from dt in dc.tblPrograms
                                                where dt.Id ==  1
                                                select dt).FirstOrDefault();

            if (existingProgram != null)
            {
                dc.tblPrograms.Remove(existingProgram);
                dc.SaveChanges();
            }

            tblProgram deletedProgram = (from dt in dc.tblPrograms
                                                where dt.Id ==  1
                                                select dt).FirstOrDefault();

            Assert.IsNull(deletedProgram);
            
        }
    }
}

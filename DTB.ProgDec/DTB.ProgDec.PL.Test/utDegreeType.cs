using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.PL;
using System.Linq;
using System.Data.Entity;

namespace DTB.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
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
        public void LoadDegreeTypesTest()
        {


            int expected = 4;
            int actual = 0;

            var degreeTypes = dc.tblDegreeTypes;

            actual = degreeTypes.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadDegreeTypesLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblDegreeType
            var degreeTypes = from dt in dc.tblDegreeTypes
                              select dt;

            actual = degreeTypes.Count();

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
                tblDegreeType newrow = new tblDegreeType();

                //set column values
                newrow.Id = -98;
                newrow.Description = "My New DegreeType";

                // Insert of the row
                dc.tblDegreeTypes.Add(newrow);

                //commit the changes (insert a row)
                // then return the rows affected
                int rowsaffected = dc.SaveChanges();

                Assert.AreNotEqual(0, rowsaffected);


            
        }

        [TestMethod]
        public void UpdateTest()
        {
            
                // retrieve one degreetype
                // select * from tblDegreeType where id = -99
                tblDegreeType existingDegreeType = (from dt in dc.tblDegreeTypes
                                                   where dt.Id == 1
                                                   select dt).FirstOrDefault();

                if(existingDegreeType != null)
                {
                    //update description
                    existingDegreeType.Description = "Test";
                    dc.SaveChanges();
                }

                tblDegreeType updatedDegreeType = (from dt in dc.tblDegreeTypes
                                                    where dt.Id == 1
                                                    select dt).FirstOrDefault();

                Assert.AreEqual(existingDegreeType.Description, updatedDegreeType.Description);
            
        } 

        [TestMethod]
        public void DeleteTest()
        {
           
                // retrieve one degreetype
                // select * from tblDegreeType where id = -99
                tblDegreeType existingDegreeType = (from dt in dc.tblDegreeTypes
                                                    where dt.Id == -98
                                                    select dt).FirstOrDefault();

                if (existingDegreeType != null)
                {
                    //update description
                    dc.tblDegreeTypes.Remove(existingDegreeType);
                    dc.SaveChanges();
                }

                tblDegreeType deletedDegreeType = (from dt in dc.tblDegreeTypes
                                                   where dt.Id == -98
                                                   select dt).FirstOrDefault();

                Assert.IsNull(deletedDegreeType);
            
        }
    }
}

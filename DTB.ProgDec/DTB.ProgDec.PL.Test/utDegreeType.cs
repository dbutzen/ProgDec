using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.PL;
using System.Linq;

namespace DTB.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadDegreeTypesTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database

            ProgDecEntities dc = new ProgDecEntities();

            //What I expect to get back
            int expected = 3;
            int actual = 0;

            //Retrieve degree types from DB
            var degreeTypes = dc.tblDegreeTypes;

            actual = degreeTypes.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadDegreeTypesLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database

            ProgDecEntities dc = new ProgDecEntities();

            //What I expect to get back
            int expected = 3;
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
            using(ProgDecEntities dc = new ProgDecEntities())
            {
                // dc only exists in here
                // type = 1 row, types = all rows

                //make new row
                tblDegreeType newrow = new tblDegreeType();

                //set column values
                newrow.Id = -99;
                newrow.Description = "My New DegreeType";

                // Insert of the row
                dc.tblDegreeTypes.Add(newrow);

                //commit the changes (insert a row)
                // then return the rows affected
                int rowsaffected = dc.SaveChanges();

                Assert.AreNotEqual(0, rowsaffected);


            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using(ProgDecEntities dc = new ProgDecEntities())
            {
                // retrieve one degreetype
                // select * from tblDegreeType where id = -99
                tblDegreeType existingDegreeType = (from dt in dc.tblDegreeTypes
                                                   where dt.Id == -99
                                                   select dt).FirstOrDefault();

                if(existingDegreeType != null)
                {
                    //update description
                    existingDegreeType.Description = "Test";
                    dc.SaveChanges();
                }

                tblDegreeType updatedDegreeType = (from dt in dc.tblDegreeTypes
                                                    where dt.Id == -99
                                                    select dt).FirstOrDefault();

                Assert.AreEqual(existingDegreeType.Description, updatedDegreeType.Description);
            }
        } 

        [TestMethod]
        public void DeleteTest()
        {
            using (ProgDecEntities dc = new ProgDecEntities())
            {
                // retrieve one degreetype
                // select * from tblDegreeType where id = -99
                tblDegreeType existingDegreeType = (from dt in dc.tblDegreeTypes
                                                    where dt.Id == -99
                                                    select dt).FirstOrDefault();

                if (existingDegreeType != null)
                {
                    //update description
                    dc.tblDegreeTypes.Remove(existingDegreeType);
                    dc.SaveChanges();
                }

                tblDegreeType deletedDegreeType = (from dt in dc.tblDegreeTypes
                                                   where dt.Id == -99
                                                   select dt).FirstOrDefault();

                Assert.IsNull(deletedDegreeType);
            }
        }
    }
}

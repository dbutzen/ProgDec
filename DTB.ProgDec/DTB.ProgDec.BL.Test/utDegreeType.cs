using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using System.Collections.Generic;

namespace DTB.ProgDec.BL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadTest()
        {
            List<DegreeType> degreeTypes = new List<DegreeType>();
            degreeTypes = DegreeTypeManager.Load();
            int expected = 5;
            Assert.AreEqual(expected, degreeTypes.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            DegreeType degreeType = new DegreeType();
            degreeType.Description = "Bachelors";

            int result = DegreeTypeManager.Insert(degreeType, true);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void UpdateTest()
        {
            DegreeType degreeType = DegreeTypeManager.LoadById(3);
            degreeType.Description = "Bachelor";

            int result = DegreeTypeManager.Update(degreeType, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DegreeTypeManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
    }
}

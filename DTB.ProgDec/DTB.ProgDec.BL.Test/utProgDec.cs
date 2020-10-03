using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using DTB.ProgDec.BL.Models;
using System.Collections.Generic;

namespace DTB.ProgDec.BL.Test
{
    [TestClass]
    public class utProgDec
    {
        [TestMethod]
        public void Load()
        {
            List<Models.ProgDec> progdecs = new List<Models.ProgDec>();
            progdecs = ProgDecManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, progdecs.Count());
        }
        [TestMethod]
        public void InsertTest()
        {
            Models.ProgDec progDec = new Models.ProgDec();
            progDec.ProgramId = 12;
            progDec.StudentId = 7;

            int result = ProgDecManager.Insert(progDec, true);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Models.ProgDec progDec = ProgDecManager.LoadById(3);
            progDec.ProgramId = 12;

            int result = ProgDecManager.Update(progDec, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = ProgDecManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
    }
}

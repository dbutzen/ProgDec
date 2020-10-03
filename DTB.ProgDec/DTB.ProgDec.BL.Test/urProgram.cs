using System;
using System.Collections.Generic;
using DTB.ProgDec.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTB.ProgDec.BL.Test
{
    [TestClass]
    public class urProgram
    {
        [TestMethod]
        public void Load()
        {
            List<Program> programs = new List<Program>();
            programs = ProgramManager.Load();
            int expected = 16;
            Assert.AreEqual(expected, programs.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            Program program = new Program();
            program.Description = "TestProgram";
            program.DegreeTypeId = 1;

            int result = ProgramManager.Insert(program, true);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Program program = ProgramManager.LoadById(3);
            program.Description = "TestProgram";

            int result = ProgramManager.Update(program, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = ProgramManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
    }
}

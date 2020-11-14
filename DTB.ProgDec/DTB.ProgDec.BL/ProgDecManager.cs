using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.PL;

namespace DTB.ProgDec.BL
{
    // Static so other projects don't have to instantiate the object
    public static class ProgDecManager
    {
        // No properties in a static class

        // Insert new ProgDec
        public static int Insert(Models.ProgDec progDec, bool rollback = false)
        {
            // Insert a row
            try
            {
                int results;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    //Make a new row
                    tblProgDec row = new tblProgDec();

                    //Set the properties
                    row.Id = dc.tblProgDecs.Any() ? dc.tblProgDecs.Max(pd => pd.Id) + 1 : 1;
                    row.ProgramId = progDec.ProgramId;
                    row.StudentId = progDec.StudentId;
                    row.ChangeDate = DateTime.Now;
                    progDec.Id = row.Id;

                    progDec.Id = row.Id;
                    progDec.ChangeDate = row.ChangeDate;

                    // Insert the row
                    dc.tblProgDecs.Add(row);
                    results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing ProgDec
        public static int Update(Models.ProgDec progDec, bool rollback = false)
        {
            // Update the row
            try
            {
                int results;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == progDec.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.ProgramId = progDec.ProgramId;
                        row.StudentId = progDec.StudentId;
                        row.ChangeDate = DateTime.Now;

                        // Insert the row
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Delete and existing ProgDec
        public static int Delete(int id, bool rollback = false)
        {
            // delete a row
            try
            {
                int results;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    //Make a new row
                    tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == id);

                    if (row != null)
                    {

                        dc.tblProgDecs.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Retrieve all the degree types

        public static List<Models.ProgDec> Load()
        {
            return Load(null);
        }
        public static List<Models.ProgDec> Load(int? programId)
        {
            try
            {
                List<Models.ProgDec> rows = new List<Models.ProgDec>();
                using (ProgDecEntities dc = new ProgDecEntities())
                {

                    var progdecs = (from pd in dc.tblProgDecs
                                    join s in dc.tblStudents on pd.StudentId equals s.Id
                                    join p in dc.tblPrograms on pd.ProgramId equals p.Id
                                    join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                                    where (pd.ProgramId == programId || programId == null)
                                    orderby s.LastName
                                    select new
                                    {
                                        ProdDecId = pd.Id,
                                        ProgramId = p.Id,
                                        StudentId = s.Id,
                                        pd.ChangeDate,
                                        programName = p.Description,
                                        s.FirstName,
                                        s.LastName,
                                        DegreeTypeName = dt.Description
                                    }).ToList();


                    progdecs.ForEach(pd => rows.Add(new Models.ProgDec
                        {
                            Id = pd.ProdDecId,
                            ProgramId = pd.ProgramId,
                            StudentId = pd.StudentId,
                            ChangeDate = pd.ChangeDate,
                            ProgramName = pd.programName,
                            DegreeTypeName = pd.DegreeTypeName,
                            StudentName = pd.LastName + ", " + pd.FirstName

                           
                        }));
                    return rows;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Retrieve one degree type
        public static Models.ProgDec LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == id);

                    var progdec = (from pd in dc.tblProgDecs
                                    join s in dc.tblStudents on pd.StudentId equals s.Id
                                    join p in dc.tblPrograms on pd.ProgramId equals p.Id
                                    join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                                    where pd.Id == id
                                    select new
                                    {
                                        ProdDecId = pd.Id,
                                        ProgramId = p.Id,
                                        StudentId = s.Id,
                                        pd.ChangeDate,
                                        programName = p.Description,
                                        s.FirstName,
                                        s.LastName,
                                        DegreeTypeName = dt.Description
                                    }).FirstOrDefault();

                    if (progdec != null)
                    {
                        Models.ProgDec progDec = new Models.ProgDec {
                            Id = progdec.ProdDecId,
                            ProgramId = progdec.ProgramId,
                            StudentId = progdec.StudentId,
                            ChangeDate = progdec.ChangeDate,
                            ProgramName = progdec.programName,
                            DegreeTypeName = progdec.DegreeTypeName,
                            StudentName = progdec.LastName + ", " + progdec.FirstName
                        };
                        return progDec;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Advisor> LoadAdvisors(int progDecId)
        {
            return AdvisorManager.Load(progDecId);
        }
    }
}

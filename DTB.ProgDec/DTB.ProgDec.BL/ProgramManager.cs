using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.PL;

namespace DTB.ProgDec.BL
{
    // Static so other projects don't have to instantiate the object
    public static class ProgramManager
    {
        // No properties in a static class

        // Insert new Program
        public static int Insert(Program program)
        {
            // Insert a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgram row = new tblProgram();

                    //Set the properties
                    row.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(p => p.Id) + 1 : 1;
                    row.Description = program.Description;
                    row.DegreeTypeId = program.DegreeTypeId;

                    // Insert the row
                    dc.tblPrograms.Add(row);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing Program
        public static int Update(Program program)
        {
            // Update the row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgram row = dc.tblPrograms.FirstOrDefault(p => p.Id == program.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = program.Description;
                        row.DegreeTypeId = program.DegreeTypeId;
                        // Insert the row
                        return dc.SaveChanges();
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
        // Delete and existing Program
        public static int Delete(int id)
        {
            // delete a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgram row = dc.tblPrograms.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {
                        dc.tblPrograms.Remove(row);
                        return dc.SaveChanges();
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
        // Retrieve all the degree types
        public static List<Program> Load()
        {
            try
            {
                List<Program> rows = new List<Program>();
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    dc.tblPrograms
                        .ToList()
                        .ForEach(p => rows.Add(new Program
                        {
                            Id = p.Id,
                            DegreeTypeId = p.DegreeTypeId,
                            Description = p.Description
                        }));;;
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Retrieve one degree type
        public static Program LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblProgram row = dc.tblPrograms.FirstOrDefault(p => p.Id == id);
                    if (row != null)
                    {
                        Program program = new Program { Id = row.Id, Description = row.Description, DegreeTypeId = row.DegreeTypeId };
                        return program;
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
    }
}

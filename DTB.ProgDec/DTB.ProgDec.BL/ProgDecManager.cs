﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public static int Insert(Models.ProgDec progDec)
        {
            // Insert a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgDec row = new tblProgDec();

                    //Set the properties
                    row.Id = dc.tblProgDecs.Any() ? dc.tblProgDecs.Max(pd => pd.Id) + 1 : 1;
                    row.ProgramId = progDec.ProgramId;
                    row.StudentId = progDec.StudentId;
                    row.ChangeDate = DateTime.Now;

                    progDec.Id = row.Id;
                    progDec.ChangeDate = row.ChangeDate;

                    // Insert the row
                    dc.tblProgDecs.Add(row);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing ProgDec
        public static int Update(Models.ProgDec progDec)
        {
            // Update the row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == progDec.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.ProgramId = progDec.ProgramId;
                        row.StudentId = progDec.StudentId;
                        row.ChangeDate = DateTime.Now;

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
        // Delete and existing ProgDec
        public static int Delete(int id)
        {
            // delete a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == id);

                    if (row != null)
                    {
                        dc.tblProgDecs.Remove(row);
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
        public static List<Models.ProgDec> Load()
        {
            try
            {
                List<Models.ProgDec> rows = new List<Models.ProgDec>();
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    dc.tblProgDecs
                        .ToList()
                        .ForEach(pd => rows.Add(new Models.ProgDec
                        {
                            Id = pd.Id,
                            ProgramId = pd.ProgramId,
                            StudentId = pd.StudentId,
                            ChangeDate = pd.ChangeDate
                           
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
                    tblProgDec row = dc.tblProgDecs.FirstOrDefault(pd => pd.Id == id);
                    if (row != null)
                    {
                        Models.ProgDec progDec = new Models.ProgDec { Id = row.Id, ProgramId = row.ProgramId, ChangeDate = row.ChangeDate, StudentId = row.StudentId };
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
    }
}

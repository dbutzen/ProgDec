﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public static int Insert(Program program, bool rollback = false)
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
                    tblProgram row = new tblProgram();

                    //Set the properties
                    row.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(p => p.Id) + 1 : 1;
                    row.Description = program.Description;
                    row.DegreeTypeId = program.DegreeTypeId;

                    // Insert the row
                    dc.tblPrograms.Add(row);
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

        // Update an existing Program
        public static int Update(Program program, bool rollback = false)
        {
            // Update the row
            try
            {
                int results;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    //Make a new row
                    tblProgram row = dc.tblPrograms.FirstOrDefault(p => p.Id == program.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = program.Description;
                        row.DegreeTypeId = program.DegreeTypeId;

                        program.Id = row.Id;
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
        // Delete and existing Program
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
                    tblProgram row = dc.tblPrograms.FirstOrDefault(p => p.Id == id);

                    if (row != null)
                    {
                        dc.tblPrograms.Remove(row);
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

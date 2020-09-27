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
    public static class DegreeTypeManager
    {
        // No properties in a static class

        // Insert new DegreeType
        public static int Insert(DegreeType degreeType)
        {
            // Insert a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblDegreeType row = new tblDegreeType();

                    //Set the properties
                    row.Id = dc.tblDegreeTypes.Any() ? dc.tblDegreeTypes.Max(dt => dt.Id) + 1 : 1;
                    row.Description = degreeType.Description;

                    // Insert the row
                    dc.tblDegreeTypes.Add(row);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing DegreeType
        public static int Update(DegreeType degreeType)
        {
            // Update the row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblDegreeType row = dc.tblDegreeTypes.FirstOrDefault(DTB => DTB.Id == degreeType.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = degreeType.Description;

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
        // Delete and existing DegreeType
        public static int Delete(int id)
        {
            // delete a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblDegreeType row = dc.tblDegreeTypes.FirstOrDefault(DTB => DTB.Id == id);

                    if (row != null)
                    {
                        dc.tblDegreeTypes.Remove(row);
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
        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> rows = new List<DegreeType>();
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    dc.tblDegreeTypes
                        .ToList()
                        .ForEach(dt => rows.Add(new DegreeType { 
                            Id = dt.Id,
                            Description = dt.Description
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
        public static DegreeType LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDegreeType row = dc.tblDegreeTypes.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        DegreeType degreeType = new DegreeType { Id = row.Id, Description = row.Description };
                        return degreeType;
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

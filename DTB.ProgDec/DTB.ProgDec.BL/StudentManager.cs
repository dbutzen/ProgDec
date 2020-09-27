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
    public static class StudentManager
    {
        // No properties in a static class

        // Insert new Student
        public static int Insert(Student student)
        {
            // Insert a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblStudent row = new tblStudent();

                    //Set the properties
                    row.Id = dc.tblStudents.Any() ? dc.tblStudents.Max(s => s.Id) + 1 : 1;
                    row.FirstName = student.FirstName;
                    row.LastName = student.LastName;
                    row.StudentId = student.StudentId;

                    student.Id = row.Id;

                    // Insert the row
                    dc.tblStudents.Add(row);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing Student
        public static int Update(Student student)
        {
            // Update the row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblStudent row = dc.tblStudents.FirstOrDefault(s => s.Id == student.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.FirstName = student.FirstName;
                        row.LastName = student.LastName;
                        row.StudentId = student.StudentId;

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
        // Delete and existing Student
        public static int Delete(int id)
        {
            // delete a row
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblStudent row = dc.tblStudents.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        dc.tblStudents.Remove(row);
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
        public static List<Student> Load()
        {
            try
            {
                List<Student> rows = new List<Student>();
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    dc.tblStudents
                        .ToList()
                        .ForEach(s => rows.Add(new Student
                        {
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            StudentId = s.StudentId
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
        public static Student LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudent row = dc.tblStudents.FirstOrDefault(s => s.Id == id);
                    if (row != null)
                    {
                        Student student = new Student { Id = row.Id, LastName = row.LastName, FirstName = row.FirstName, StudentId = row.StudentId };
                        return student;
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

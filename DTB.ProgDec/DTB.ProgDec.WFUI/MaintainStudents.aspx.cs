using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using System;
using System.Collections.Generic;

namespace DTB.ProgDec.WFUI
{
    public partial class MaintainStudents : System.Web.UI.Page
    {
        List<Student> students;
        Student student;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                students = new List<Student>();
                students = StudentManager.Load();

                Rebind();


                Session["students"] = students;

            }
            else
            {
                students = (List<Student>)Session["students"];
            }
        }

        private void Rebind()
        {
            ddlStudents.DataSource = students;
            ddlStudents.DataTextField = "FullName";
            ddlStudents.DataValueField = "Id";
            ddlStudents.DataBind();

        }

        protected void ddlStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            student = students[ddlStudents.SelectedIndex];

            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtStudentId.Text = student.StudentId;
        }


        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                student = new Student();
                // Get typed description from screen
                student.FirstName = txtFirstName.Text;
                student.LastName = txtLastName.Text;
                student.StudentId = txtStudentId.Text;

                // Add to database
                StudentManager.Insert(student);

                // Add to list
                students.Add(student);
                Session["students"] = students;
                Rebind();
                // Select new student
                ddlStudents.SelectedValue = student.Id.ToString();


            }
            catch (Exception ex)
            {

                message.Text = ex.Message;
                message.CssClass = "text-danger";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ddlStudents.SelectedIndex;

                student = students[index];
                // Get typed description from screen
                student.FirstName = txtFirstName.Text;
                student.LastName = txtLastName.Text;
                student.StudentId = txtStudentId.Text;

                // Add to database
                StudentManager.Update(student);

                students[index] = student;
                Session["students"] = students;

                Rebind();

                // Select new student
                ddlStudents.SelectedValue = student.Id.ToString();

                // Force the event to fire
                ddlStudents_SelectedIndexChanged(sender, e);


            }
            catch (Exception ex)
            {

                message.Text = ex.Message;
                message.CssClass = "text-danger";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                // Delete from database
                StudentManager.Delete(students[ddlStudents.SelectedIndex].Id);

                // Add to list
                students.Remove(students[ddlStudents.SelectedIndex]);
                Rebind();


            }
            catch (Exception ex)
            {

                message.Text = ex.Message;
                message.CssClass = "text-danger";
            }
        }
    }
}
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;
using System;
using System.Collections.Generic;

namespace DTB.ProgDec.WFUI
{
    public partial class MaintainPrograms : System.Web.UI.Page
    {
        List<Program> programs;
        List<DegreeType> degreeTypes;
        Program program;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                programs = new List<Program>();
                programs = ProgramManager.Load();

                degreeTypes = new List<DegreeType>();
                degreeTypes = DegreeTypeManager.Load();

                Rebind();


                Session["programs"] = programs;
                Session["degreetypes"] = degreeTypes;

            }
            else
            {
                programs = (List<Program>)Session["programs"];
                degreeTypes = (List<DegreeType>)Session["degreetypes"];
            }
        }

        private void Rebind()
        {
            ddlPrograms.DataSource = programs;
            ddlPrograms.DataTextField = "Description";
            ddlPrograms.DataValueField = "Id";
            ddlPrograms.DataBind();

            ddlDegreeTypes.DataSource = degreeTypes;
            ddlDegreeTypes.DataTextField = "Description";
            ddlDegreeTypes.DataValueField = "Id";
            ddlDegreeTypes.DataBind();
        }

        protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            program = programs[ddlPrograms.SelectedIndex];
            txtDescription.Text = program.Description;

            ddlDegreeTypes.SelectedValue = program.DegreeTypeId.ToString();
        }

        protected void ddlDegreeTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                program = new Program();
                // Get typed description from screen
                program.Description = txtDescription.Text;
                program.DegreeTypeId = degreeTypes[ddlDegreeTypes.SelectedIndex].Id;

                // Add to database
                ProgramManager.Insert(program);

                // Add to list
                programs.Add(program);
                Session["programs"] = programs;
                Rebind();
                // Select new program
                ddlPrograms.SelectedValue = program.Id.ToString();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ddlPrograms.SelectedIndex;

                program = programs[index];
                // Get typed description from screen
                program.Description = txtDescription.Text;
                program.DegreeTypeId = degreeTypes[ddlDegreeTypes.SelectedIndex].Id;

                // Add to database
                ProgramManager.Update(program);

                programs[index] = program;
                Session["programs"] = programs;

                Rebind();

                // Select new program
                ddlPrograms.SelectedValue = program.Id.ToString();

                // Force the event to fire
                ddlPrograms_SelectedIndexChanged(sender, e);


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                // Delete from database
                ProgramManager.Delete(programs[ddlPrograms.SelectedIndex].Id);

                // Add to list
                programs.Remove(programs[ddlPrograms.SelectedIndex]);
                Rebind();
                txtDescription.Text = string.Empty;


            }
            catch (Exception ex)
            {

                message.Text = ex.Message;
                message.CssClass = "text-danger";
            }
        }
    }
}
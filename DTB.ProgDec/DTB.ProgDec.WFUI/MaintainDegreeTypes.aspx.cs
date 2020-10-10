using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.WFUI
{
    public partial class MaintainDegreeTypes : System.Web.UI.Page
    {
        List<DegreeType> degreeTypes;
        DegreeType degreeType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //If not postback (here for the first time)

            if (!IsPostBack)
            {
                // Call the correct load method in the BL
                degreeTypes = new List<DegreeType>();
                degreeTypes = DegreeTypeManager.Load();
                Rebind();

                // put into session
                Session["degreetypes"] = degreeTypes;
            }
            else
            {
                // Load from session
                degreeTypes = (List<DegreeType>)Session["degreetypes"];
            }

        }

        private void Rebind()
        {
            // Bind the list to the dropdownlist
            ddlDegreeTypes.DataSource = null;
            ddlDegreeTypes.DataSource = degreeTypes;

            // Designate the display field
            ddlDegreeTypes.DataTextField = "Description";

            // Designate the primary key field
            ddlDegreeTypes.DataValueField = "Id";

            // Refresh the binding
            ddlDegreeTypes.DataBind();

            txtDescription.Text = string.Empty;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                degreeType = new DegreeType();
                // Get typed description from screen
                degreeType.Description = txtDescription.Text;

                // Add to database
                DegreeTypeManager.Insert(degreeType);

                // Add to list
                degreeTypes.Add(degreeType);
                Rebind();


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
                int index = ddlDegreeTypes.SelectedIndex;
                // Get the one the user selected
                degreeType = degreeTypes[ddlDegreeTypes.SelectedIndex];
                // Get typed description from screen
                degreeType.Description = txtDescription.Text;

                // Add to database
                DegreeTypeManager.Update(degreeType);

                // Update the list
                degreeTypes[ddlDegreeTypes.SelectedIndex] = degreeType;
                Rebind();


                //ddlDegreeTypes.SelectedIndex = index;
                ddlDegreeTypes_SelectedIndexChanged(sender, e);


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
                // Get the one the user selected
                degreeType = degreeTypes[ddlDegreeTypes.SelectedIndex];

                // Add to database
                DegreeTypeManager.Delete(degreeType.Id);

                // Update the list
                degreeTypes.Remove(degreeType);
                Rebind();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }

        protected void ddlDegreeTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GEt selected degree type
            degreeType = degreeTypes[ddlDegreeTypes.SelectedIndex];

            //Display
            txtDescription.Text = degreeType.Description;
        }
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin_Defects : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (Session.Count == 0)
         Response.Redirect("~/Admin/AdminLogin.aspx");
      else
      {
         if (!this.IsPostBack)
         {
            this.defectDescriptionTextBox.Text = "";
            this.defectNotesTextBox.Text = "";
         }
      }
   }

   protected void adminDefectsGridView_SelectedIndexChanged(object sender, EventArgs e)
   {
      GridView gv = (GridView)sender;
      int defectId = (int)gv.SelectedDataKey.Value;
      DefectsDataAccess defectsDataAccess = new DefectsDataAccess();
      this.defectDescriptionTextBox.Text = defectsDataAccess.GetDefectDescription(defectId);
      this.defectNotesTextBox.Text = defectsDataAccess.GetDefectNotes(defectId);
   }

   protected void adminDefectsGridView_RowEditing(object sender, GridViewEditEventArgs e)
   {
      GridView gv = (GridView)sender;
      string defectIdStr = gv.Rows[e.NewEditIndex].Cells[1].Text;
      try
      {
         int defectId = Convert.ToInt32(defectIdStr);
         Session.Add("DefectId", defectId);
         e.Cancel = true;
         Response.Redirect("~/Admin/EditDefect.aspx");
      }
      catch (Exception)
      {
      }
   }

   protected void adminDefectsGridView_PageIndexChanged(object sender, EventArgs e)
   {
      this.defectDescriptionTextBox.Text = "";
      this.defectNotesTextBox.Text = "";
   }
}

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

public partial class BetaDefectReporting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session.Count == 0)
       {
          Response.Redirect("~/BetaLogin.aspx");
       }
       else
       {
          this.newDefectLabel.Text = "";
       }
    }

   protected void addDefectButton_Click(object sender, EventArgs e)
   {
      if (this.defectTitleTextBox.Text.Trim().Equals("") || this.defectDescriptionTextBox.Text.Trim().Equals(""))
      {
         this.newDefectLabel.Text = "No blank fields please!";
      }
      else
      {
         string testerID = (string)Session["UserId"];
         string defectTitle = this.defectTitleTextBox.Text;
         string defectDescription = this.defectDescriptionTextBox.Text;
         string notes = "";
         bool isFixed = false;
         bool isPostponed = false;
         DateTime dateCreated = DateTime.Now;
         DateTime dateFixed = DateTime.MaxValue;
         DefectsDataAccess defectsDataAccess = new DefectsDataAccess();
         defectsDataAccess.InsertDefect(testerID, defectTitle, defectDescription, notes, isFixed, isPostponed, dateCreated, dateFixed);
         this.defectTitleTextBox.Text = "";
         this.defectDescriptionTextBox.Text = "";
      }
   }
}

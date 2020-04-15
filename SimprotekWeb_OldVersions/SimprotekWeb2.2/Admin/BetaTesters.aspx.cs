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

public partial class Admin_BetaTesters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session.Count == 0)
          Response.Redirect("~/Admin/AdminLogin.aspx");
       else
       {
          if (!this.IsPostBack)
          {
             this.testerNotesTextBox.Text = "";
          }
       }
    }

   protected void betaTestersGridView_SelectedIndexChanged(object sender, EventArgs e)
   {
      GridView gv = (GridView)sender;
      string testerId = (string)gv.SelectedDataKey.Value;
      TestersDataAccess testersDataAccess = new TestersDataAccess();
      this.testerNotesTextBox.Text = testersDataAccess.GetTesterNotes(testerId);
   }

   protected void betaTestersGridView_RowEditing(object sender, GridViewEditEventArgs e)
   {
      GridView gv = (GridView)sender;
      string testerId = gv.Rows[e.NewEditIndex].Cells[1].Text;
      Session.Add("TesterId", testerId);
      e.Cancel = true;
      Response.Redirect("~/Admin/EditBetaTester.aspx");
   }

   protected void newBetaTesterButton_Click(object sender, EventArgs e)
   {
      Response.Redirect("~/Admin/NewBetaTester.aspx");
   }

   protected void betaTestersGridView_PageIndexChanged(object sender, EventArgs e)
   {
      this.testerNotesTextBox.Text = "";
   }
}

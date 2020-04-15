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

public partial class BetaYourDefects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session.Count == 0)
          Response.Redirect("~/BetaLogin.aspx");

    }

   protected void yourDefectsGridView_SelectedIndexChanged(object sender, EventArgs e)
   {
      GridView gv = (GridView)sender;
      int defectID = (int)gv.SelectedDataKey.Value;
      DefectsDataAccess defectsDataAccess = new DefectsDataAccess();
      this.defectDescriptionTextBox.Text = defectsDataAccess.GetDefectDescription(defectID);
   }
}

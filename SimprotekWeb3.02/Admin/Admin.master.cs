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

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session.Count > 0)
       {
          this.loggedUserLabel.Text = (string)Session["UserId"];
       }
       else
       {
          this.loggedUserLabel.Text = "";
       }
    }

   protected void logoutLinkButton_Click(object sender, EventArgs e)
   {
      Session.Clear();
      this.loggedUserLabel.Text = "";
   }
}

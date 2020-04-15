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

public partial class Admin_NewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       this.newUserMessageLabel.Text = "";
       if (Session.Count == 0)
          Response.Redirect("~/Admin/AdminLogin.aspx");

    }

   protected void newUserDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
   {
      IEnumerator en = e.Values.Values.GetEnumerator();
      while (en.MoveNext())
      {
         string val = (string)en.Current;
         if (val == null || val.Trim().Equals(""))
         {
            e.Cancel = true;
            this.newUserMessageLabel.Text = "No blank fields please!";
            break;
         }
      }

   }

   protected void newUserDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
   {
      this.newUserMessageLabel.Text = "";
   }
}

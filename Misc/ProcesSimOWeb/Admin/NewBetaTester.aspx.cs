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

public partial class Admin_NewBetaTester : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session.Count == 0)
          Response.Redirect("~/Admin/AdminLogin.aspx");
       else
       {
          this.newTesterMessageLabel.Text = "";
       }
    }

   protected void newBetaTesterDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
   {
      bool okInserting = true;
      IEnumerator en = e.Values.Values.GetEnumerator();
      while (en.MoveNext())
      {
         string val = (string)en.Current;
         if (val == null || val.Trim().Equals(""))
         {
            e.Cancel = true;
            this.newTesterMessageLabel.Text = "No blank fields please!";
            okInserting = false;
            break;
         }
      }
      if (okInserting)
      {
         DateTime testerCreationDate = DateTime.Now;
         Session.Add("TesterCreationDate", testerCreationDate.ToString());
      }
   }
   
   protected void newBetaTesterDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
   {
      this.newTesterMessageLabel.Text = "";
   }

   protected void newBetaTesterAccessDataSource_Inserted(object sender, SqlDataSourceStatusEventArgs e)
   {
      Session.Remove("TesterCreationDate");
   }
}

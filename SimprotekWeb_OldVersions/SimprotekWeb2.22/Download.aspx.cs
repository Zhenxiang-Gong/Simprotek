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

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          //this.messageLabel.Text = "";
       }
    }

   //protected void loginButton_Click(object sender, EventArgs e)
   // {
   //    Session.Clear();
   //    if (this.userIdTextBox.Text.Trim().Equals(""))
   //    {
   //       this.messageLabel.Text = "Please specify a user name!";
   //    }
   //    else if (this.userPasswordTextBox.Text.Trim().Equals(""))
   //    {
   //       this.messageLabel.Text = "Please specify a password!";
   //    }
   //    else
   //    {
   //       UsersDataAccess usersDataAccess = new UsersDataAccess();
   //       string id = this.userIdTextBox.Text;
   //       string pwd = this.userPasswordTextBox.Text;
   //       AmscoUser user = usersDataAccess.AuthenticateUser(id, pwd);
   //       if (user.IsAuthenticated)
   //       {
   //          if (user.Role.Equals("Beta Tester"))
   //          {
   //             Session["UserId"] = user.Id;
   //             Session["UserRole"] = user.Role;
   //             Response.Redirect("~/Beta/BetaMain.aspx");
   //          }
   //          else
   //          {
   //             this.messageLabel.Text = "You are not authorized!";
   //          }
   //       }
   //       else
   //       {
   //          this.messageLabel.Text = "You are not authenticated!";
   //       }
   //    }       
    //}
}

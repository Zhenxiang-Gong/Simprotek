using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AmscoUser
/// </summary>
public class AmscoUser
{
   public const string ROLE_ADMIN = "Administrator"; //not working
   public const string ROLE_BETA_TESTER = "Beta Tester"; //not working

   private string userId;
   private string userPassword;
   private string userRole;
   private bool isAuthenticated;

   public string Id
   {
      get { return userId;}
   }

   public string Password
   {
      get { return userPassword;}
   }

   public string Role
   {
      get { return userRole;}
   }

   public bool IsAuthenticated
   {
      get { return isAuthenticated; }
   }

   public AmscoUser(string id, string password, string role, bool authenticated)
	{
      this.userId = id;
      this.userPassword = password;
      this.userRole = role;
      this.isAuthenticated = authenticated;
	}
}

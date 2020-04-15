using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for UsersDataAccess
/// </summary>
public class UsersDataAccess
{
   private const int USER_ID_INDEX = 0;
   private const int USER_PASSWORD_INDEX = 1;
   private const int USER_ROLE_INDEX = 2;

   public UsersDataAccess()
   {
		//
		// TODO: Add constructor logic here
		//
	}

   public AmscoUser AuthenticateUser(string id, string password)
   {
      AmscoUser user = new AmscoUser("", "", "", false);
      UsersDataSet usersDataSet = this.GetUsers();
      IEnumerator en = usersDataSet.Users.GetEnumerator();
      while (en.MoveNext())
      {
         DataRow dataRow = (DataRow)en.Current;
         string userId = (string)dataRow.ItemArray[UsersDataAccess.USER_ID_INDEX];
         string userPassword = (string)dataRow[UsersDataAccess.USER_PASSWORD_INDEX];
         if (id.Equals(userId) && password.Equals(userPassword))
         {
            string userRole = (string)dataRow[UsersDataAccess.USER_ROLE_INDEX];
            user = new AmscoUser(userId, userPassword, userRole, true);
            break;
         }
      }
      return user;
   }

   public UsersDataSet GetUsers()
   {
      UsersDataSetTableAdapters.UsersTableAdapter usersTableAdapter = new UsersDataSetTableAdapters.UsersTableAdapter();
      UsersDataSet usersDataSet = new UsersDataSet();
      usersTableAdapter.Fill(usersDataSet.Users);
      return usersDataSet;
   }

}

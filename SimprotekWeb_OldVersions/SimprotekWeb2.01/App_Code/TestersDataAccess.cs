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
/// Summary description for TestersDataAccess
/// </summary>
public class TestersDataAccess
{
	public TestersDataAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   public string GetTesterNotes(string testerId)
   {
      TesterNotesDataSetTableAdapters.TesterNotesTableAdapter testerNotesTableAdapter = new TesterNotesDataSetTableAdapters.TesterNotesTableAdapter();
      TesterNotesDataSet testerNotesDataSet = new TesterNotesDataSet();
      testerNotesDataSet.EnforceConstraints = false;
      testerNotesTableAdapter.Fill(testerNotesDataSet.Testers, testerId);
      string testerNotes = (string)testerNotesDataSet.Testers.Rows[0].ItemArray.GetValue(0);
      return testerNotes;
   }

   public void UpdateTester(string companyName, string firstName, string lastName, string address,
         string city, string stateOrProvince, string postalCode, string country, string phoneNumber, string extension,
         string faxNumber, string emailAddress, string notes, DateTime creationDate, string origTesterId)
   {
      UpdateTesterDataSetTableAdapters.UpdateTesterTableAdapter updateTesterTableAdapter = new UpdateTesterDataSetTableAdapters.UpdateTesterTableAdapter();
      updateTesterTableAdapter.Update(companyName, firstName, lastName, address, city, stateOrProvince,
         postalCode, country, phoneNumber, extension, faxNumber, emailAddress, notes, creationDate, origTesterId);
   }

   public OneTesterDataSet GetTester(string testerId)
   {
      OneTesterDataSetTableAdapters.OneTesterTableAdapter oneTesterTableAdapter = new OneTesterDataSetTableAdapters.OneTesterTableAdapter();
      OneTesterDataSet oneTesterDataSet = new OneTesterDataSet();
      oneTesterDataSet.EnforceConstraints = false;
      oneTesterTableAdapter.Fill(oneTesterDataSet.Testers, testerId);
      return oneTesterDataSet;
   }
}

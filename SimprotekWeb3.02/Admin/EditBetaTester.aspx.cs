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

public partial class Admin_EditBetaTester : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (Session.Count == 0)
         Response.Redirect("~/Admin/AdminLogin.aspx");
      else
      {
         if (!IsPostBack)
         {
            this.editTesterMessageLabel.Text = "";
            string testerId = (string)Session["TesterId"];
            TestersDataAccess testersDataAccess = new TestersDataAccess();
            OneTesterDataSet oneTesterDataSet = testersDataAccess.GetTester(testerId);
            this.testerIdLabel.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(0);
            this.companyNameTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(1);
            this.firstNameTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(2);
            this.lastNameTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(3);
            this.addressTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(4);
            this.cityTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(5);
            this.stateOrProvinceTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(6);
            this.postalCodeTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(7);
            this.countryTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(8);
            this.phoneNumberTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(9);
            this.extensionTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(10);
            this.faxNumberTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(11);
            this.emailAddressTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(12);
            this.testerNotesTextBox.Text = (string)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(13);
            this.creationDateLabel.Text = ((DateTime)oneTesterDataSet.Testers.Rows[0].ItemArray.GetValue(14)).ToString();
         }
      }
   }

   protected void editTesterUpdateButton_Click(object sender, EventArgs e)
   {
      TestersDataAccess testersDataAccess = new TestersDataAccess();

      string companyName = this.companyNameTextBox.Text;
      string firstName = this.firstNameTextBox.Text;
      string lastName = this.lastNameTextBox.Text;
      string address = this.addressTextBox.Text;
      string city = this.cityTextBox.Text;
      string stateOrProvince = this.stateOrProvinceTextBox.Text;
      string postalCode = this.postalCodeTextBox.Text;
      string country = this.countryTextBox.Text;
      string phoneNumber = this.phoneNumberTextBox.Text;
      string extension = this.extensionTextBox.Text;
      string faxNumber = this.faxNumberTextBox.Text;
      string emailAddress = this.emailAddressTextBox.Text;
      string notes = this.testerNotesTextBox.Text;
      DateTime creationDate = System.Convert.ToDateTime(this.creationDateLabel.Text);

      string origTesterId = (string)Session["TesterId"];

      testersDataAccess.UpdateTester(companyName, firstName, lastName, address, city, stateOrProvince,
         postalCode, country, phoneNumber, extension, faxNumber, emailAddress, notes, creationDate, origTesterId);

      Session.Remove("TesterId");
      Response.Redirect("~/Admin/BetaTesters.aspx");
   }

   protected void editTesterCancelButton_Click(object sender, EventArgs e)
   {
      Session.Remove("TesterId");
      Response.Redirect("~/Admin/BetaTesters.aspx");
   }
}

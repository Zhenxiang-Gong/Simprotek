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

public partial class Admin_EditDefect : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (Session.Count == 0)
         Response.Redirect("~/Admin/AdminLogin.aspx");
      else
      {
         if (!IsPostBack)
         {
            int defectId = (int)Session["DefectId"];
            DefectsDataAccess defectsDataAccess = new DefectsDataAccess();
            OneDefectDataSet oneDefectDataSet = defectsDataAccess.GetDefect(defectId);
            this.defectIdLabel.Text = ((int)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(0)).ToString();
            this.testerIdLabel.Text = (string)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(1);
            this.defectTitleLabel.Text = (string)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(2);

            this.defectDescriptionTextBox.Text = (string)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(3);
            this.defectNotesTextBox.Text = (string)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(4);

            this.fixedCheckBox.Checked = (bool)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(5);
            this.postponedCheckBox.Checked = (bool)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(6);

            DateTime dateCreated = (DateTime)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(7);
            this.dateCreatedLabel.Text = dateCreated.ToString();

            DateTime dateFixed = (DateTime)oneDefectDataSet.Defects.Rows[0].ItemArray.GetValue(8);
            this.dateFixedLabel.Text = dateFixed.ToString();
         }
      }
   }
   
   protected void editDefectUpdateButton_Click(object sender, EventArgs e)
   {
      DefectsDataAccess defectsDataAccess = new DefectsDataAccess();
      
      string testerId =  this.testerIdLabel.Text;     
      string defectTitle = this.defectTitleLabel.Text;
      
      string defectDescription = this.defectDescriptionTextBox.Text;
      string notes = this.defectNotesTextBox.Text;

      bool isFixed = this.fixedCheckBox.Checked;
      bool isPostponed = this.postponedCheckBox.Checked;

      DateTime dateCreated = System.Convert.ToDateTime(this.dateCreatedLabel.Text);
      DateTime dateFixed = System.Convert.ToDateTime(this.dateFixedLabel.Text);

      int origDefectId = (int)Session["DefectId"];
      
      defectsDataAccess.UpdateDefect(testerId, defectTitle, defectDescription, notes, isFixed, isPostponed, dateCreated, dateFixed, origDefectId);
      
      Session.Remove("DefectId");
      Response.Redirect("~/Admin/Defects.aspx");
   }

   protected void editDefectCancelButton_Click(object sender, EventArgs e)
   {
      Session.Remove("DefectId");
      Response.Redirect("~/Admin/Defects.aspx");
   }

   protected void fixedCheckBox_CheckedChanged(object sender, EventArgs e)
   {
      if (this.fixedCheckBox.Checked)
      {
         this.postponedCheckBox.Checked = false;
         this.dateFixedLabel.Text = DateTime.Now.ToString();
      }
      else
      {
         this.dateFixedLabel.Text = DateTime.MaxValue.ToString();
      }
   }
   
   protected void postponedCheckBox_CheckedChanged(object sender, EventArgs e)
   {
      if (this.postponedCheckBox.Checked)
      {
         this.dateFixedLabel.Text = DateTime.MaxValue.ToString();
         this.fixedCheckBox.Checked = false;
      }
   }
}

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
/// Summary description for DefectsDataAccess
/// </summary>
public class DefectsDataAccess
{
	public DefectsDataAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   public string GetDefectDescription(int defectId)
   {
      DefectDescriptionDataSetTableAdapters.DefectDescriptionTableAdapter defectDescrTableAdapter = new DefectDescriptionDataSetTableAdapters.DefectDescriptionTableAdapter();
      DefectDescriptionDataSet defectDescriptionDataSet = new DefectDescriptionDataSet();
      defectDescriptionDataSet.EnforceConstraints = false;
      defectDescrTableAdapter.Fill(defectDescriptionDataSet.Defects, defectId);
      string defectDescr = (string)defectDescriptionDataSet.Defects.Rows[0].ItemArray.GetValue(0);
      return defectDescr;
   }

   public string GetDefectNotes(int defectId)
   {
      DefectNotesDataSetTableAdapters.DefectNotesTableAdapter defectNotesTableAdapter = new DefectNotesDataSetTableAdapters.DefectNotesTableAdapter();
      DefectNotesDataSet defectNotesDataSet = new DefectNotesDataSet();
      defectNotesDataSet.EnforceConstraints = false;
      defectNotesTableAdapter.Fill(defectNotesDataSet.Defects, defectId);
      string defectNotes = (string)defectNotesDataSet.Defects.Rows[0].ItemArray.GetValue(0);
      return defectNotes;
   }

   public DefectsDataSet GetAllDefects()
   {
      DefectsDataSetTableAdapters.DefectsTableAdapter defectsTableAdapter = new DefectsDataSetTableAdapters.DefectsTableAdapter();
      DefectsDataSet defectsDataSet = new DefectsDataSet();
      defectsDataSet.EnforceConstraints = false;
      defectsTableAdapter.Fill(defectsDataSet.Defects);
      return defectsDataSet;
   }

   public void InsertDefect(string testerId, string defectTitle, string defectDescription, string notes, bool isFixed, bool isPostponed, DateTime dateCreated, DateTime dateFixed)
   {
      DefectsDataSetTableAdapters.DefectsTableAdapter defectsTableAdapter = new DefectsDataSetTableAdapters.DefectsTableAdapter();
      defectsTableAdapter.Insert(testerId, defectTitle, defectDescription, notes, isFixed, isPostponed, dateCreated, dateFixed);
   }

   public void UpdateDefect(string testerId, string defectTitle, string defectDescription, string notes, bool isFixed, bool isPostponed, DateTime dateCreated, DateTime dateFixed, int origDefectId)
   {
      DefectsDataSetTableAdapters.DefectsTableAdapter defectsTableAdapter = new DefectsDataSetTableAdapters.DefectsTableAdapter();
      defectsTableAdapter.Update(testerId, defectTitle, defectDescription, notes, isFixed, isPostponed, dateCreated, dateFixed, origDefectId);
   }

   public void DeleteDefect(int origDefectId)
   {
      DefectsDataSetTableAdapters.DefectsTableAdapter defectsTableAdapter = new DefectsDataSetTableAdapters.DefectsTableAdapter();
      defectsTableAdapter.Delete(origDefectId);
   }

   public OneDefectDataSet GetDefect(int defectId)
   {
      OneDefectDataSetTableAdapters.OneDefectTableAdapter oneDefectTableAdapter = new OneDefectDataSetTableAdapters.OneDefectTableAdapter();
      OneDefectDataSet oneDefectDataSet = new OneDefectDataSet();
      oneDefectDataSet.EnforceConstraints = false;
      oneDefectTableAdapter.Fill(oneDefectDataSet.Defects, defectId);
      return oneDefectDataSet;
   }
}

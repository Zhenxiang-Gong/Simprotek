<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EditDefect.aspx.cs" Inherits="Admin_EditDefect" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 87px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Edit Defect"
               Width="130px"></asp:Label></td>
         <td style="width: 6500px">
         </td>
      </tr>
      <tr>
         <td style="width: 87px">
         </td>
         <td style="width: 6500px">
         </td>
      </tr>
      <tr>
         <td style="width: 87px">
         </td>
         <td style="width: 6500px">
         </td>
      </tr>
   </table>
   <table style="width: 100%">
      <tr>
         <td style="width: 100px">
            <table style="width: 100%">
               <tr>
                  <td style="width: 27px; text-align: right; height: 18px;">
            DefectId:&nbsp;
                  </td>
                  <td style="width: 100px; height: 18px;">
            <asp:Label ID="defectIdLabel" runat="server" BackColor="#E0E0E0" Width="241px"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 27px; text-align: right">
            TesterId:&nbsp;</td>
                  <td style="width: 100px">
            <asp:Label ID="testerIdLabel" runat="server" BackColor="#E0E0E0" Width="241px"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 27px; text-align: right; height: 21px;">
            DefectTitle:&nbsp;
                  </td>
                  <td style="width: 100px; height: 21px;">
            <asp:Label ID="defectTitleLabel" runat="server" BackColor="#E0E0E0" Width="936px"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 27px; text-align: right">
                  </td>
                  <td style="width: 100px">
            </td>
               </tr>
            </table>
            <table style="width: 100%">
               <tr>
                  <td style="width: 116px">
            <asp:CheckBox ID="fixedCheckBox" runat="server" AutoPostBack="True" OnCheckedChanged="fixedCheckBox_CheckedChanged"
               Text="Fixed" Width="202px" /></td>
                  <td style="width: 156px">
            <asp:CheckBox ID="postponedCheckBox" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="postponedCheckBox_CheckedChanged" Text="Postponed" /></td>
               </tr>
            </table>
            </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <table style="width: 100%">
               <tr>
                  <td style="width: 100px">
            DateCreated:&nbsp;</td>
                  <td style="width: 100px">
            DateFixed:</td>
               </tr>
               <tr>
                  <td style="width: 100px">
                     &nbsp;<asp:Label ID="dateCreatedLabel" runat="server" BackColor="#E0E0E0" Width="438px"></asp:Label></td>
                  <td style="width: 100px">
                     &nbsp;<asp:Label ID="dateFixedLabel" runat="server" BackColor="#E0E0E0" Width="438px"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 100px">
                  </td>
                  <td style="width: 100px">
                  </td>
               </tr>
               <tr>
                  <td style="width: 100px">
                     DefectDescription:</td>
                  <td style="width: 100px">
                     DefectNotes:</td>
               </tr>
               <tr>
                  <td style="width: 100px">
                     <asp:TextBox ID="defectDescriptionTextBox" runat="server" Rows="20" TextMode="MultiLine"
                        Width="500px" Wrap="False"></asp:TextBox></td>
                  <td style="width: 100px">
                     <asp:TextBox ID="defectNotesTextBox" runat="server" Rows="20" TextMode="MultiLine"
                        Width="500px" Wrap="False"></asp:TextBox></td>
               </tr>
            </table>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <asp:Button ID="editDefectUpdateButton" runat="server" OnClick="editDefectUpdateButton_Click"
               Text="Update" /><asp:Button ID="editDefectCancelButton" runat="server" OnClick="editDefectCancelButton_Click"
               Text="Cancel" /></td>
      </tr>
   </table>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Beta/Beta.master" AutoEventWireup="true" CodeFile="BetaDefectReporting.aspx.cs" Inherits="BetaDefectReporting" Title="Untitled Page" %>
<asp:Content ID="betaDefectReportingContent" ContentPlaceHolderID="betaContentPlaceHolder" Runat="Server">
   <div style="text-align: center">
      <div style="text-align: center">
         <table style="width: 100%; height: 100%">
            <tr>
               <td style="width: 100px">
               </td>
               <td align="left" style="width: 100px">
                  <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Report Defects"
                     Width="217px" style="text-align: left"></asp:Label></td>
            </tr>
            <tr>
               <td style="width: 100px">
               </td>
               <td align="center" style="width: 100px">
               </td>
            </tr>
            <tr>
               <td style="width: 100px">
               </td>
               <td align="center" style="width: 100px">
               </td>
            </tr>
            <tr>
               <td style="width: 100px">
               </td>
               <td style="width: 100px" align="center">
                  New Defect</td>
            </tr>
            <tr>
               <td style="width: 100px">
                  Title:&nbsp;</td>
               <td style="width: 100px">
                  <asp:TextBox ID="defectTitleTextBox" runat="server" Width="500px"></asp:TextBox></td>
            </tr>
            <tr>
               <td style="width: 100px">
                  Description:&nbsp;</td>
               <td style="width: 100px">
                  <asp:TextBox ID="defectDescriptionTextBox" runat="server" Rows="20" TextMode="MultiLine"
                     Width="500px"></asp:TextBox></td>
            </tr>
            <tr>
               <td style="width: 100px">
               </td>
               <td align="center" style="width: 100px">
                  <asp:Button ID="addDefectButton" runat="server" Text="Add Defect" OnClick="addDefectButton_Click" /></td>
            </tr>
            <tr>
               <td style="width: 100px; height: 21px">
               </td>
               <td align="center" style="width: 100px; height: 21px">
                  <asp:Label ID="newDefectLabel" runat="server" ForeColor="Red" Width="397px"></asp:Label></td>
            </tr>
         </table>
      </div>
      &nbsp;&nbsp;
   </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Beta/Beta.master" AutoEventWireup="true" CodeFile="BetaMain.aspx.cs" Inherits="BetaMain" Title="Untitled Page" %>
<asp:Content ID="betaMainContent" ContentPlaceHolderID="betaContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 550px; text-align: left">
         </td>
         <td style="width: 100px; text-align: left">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Beta Testing"
               Width="217px" style="text-align: left"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 250px">
            <asp:Label ID="Label1" runat="server" Text=" " Width="57px"></asp:Label></td>
         <td style="width: 100px">
   Here we need to give general info about the beta testing</td>
      </tr>
   </table>
   <br />
   <br />
</asp:Content>


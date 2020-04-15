<%@ Page Language="C#" MasterPageFile="~/Admin/InitialAdmin.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" Title="Untitled Page" %>
<asp:Content ID="adminLoginContent" ContentPlaceHolderID="initialAdminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100px; height: 21px;">
             &nbsp;User Name:</td>
          <td style="width: 100px; height: 21px">
              <asp:TextBox ID="userIdTextBox" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
         <td>
             Password:</td>
          <td>
              <asp:TextBox ID="userPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox></td>
      </tr>
       <tr>
           <td>
              <asp:Label ID="Label1" runat="server" Text=" " Width="112px"></asp:Label></td>
           <td>
               <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" /></td>
       </tr>
      <tr>
         <td style="height: 21px">
         </td>
         <td style="height: 21px">
         </td>
      </tr>
   </table>
   <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
</asp:Content>


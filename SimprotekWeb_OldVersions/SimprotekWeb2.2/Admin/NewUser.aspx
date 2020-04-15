<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="Admin_NewUser" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 114px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="New User" Width="150px"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 114px">
         </td>
      </tr>
      <tr>
         <td style="width: 114px">
         </td>
      </tr>
      <tr>
         <td style="width: 114px">
            <asp:DetailsView ID="newUserDetailsView" runat="server" AutoGenerateRows="False"
               DataKeyNames="UserId" DataSourceID="newUserAccessDataSource" DefaultMode="Insert"
               Height="50px" Width="125px" OnItemInserted="newUserDetailsView_ItemInserted" OnItemInserting="newUserDetailsView_ItemInserting">
               <Fields>
                  <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
                  <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword" />
                  <asp:BoundField DataField="UserRole" HeaderText="UserRole" SortExpression="UserRole" />
                  <asp:CommandField ShowInsertButton="True" />
               </Fields>
            </asp:DetailsView>
         </td>
      </tr>
      <tr>
         <td style="width: 114px">
            <asp:Label ID="newUserMessageLabel" runat="server" Width="250px" ForeColor="Red"></asp:Label></td>
      </tr>
   </table>
   <asp:AccessDataSource ID="newUserAccessDataSource" runat="server" ConflictDetection="CompareAllValues"
      DataFile="~/App_Data/betatesting.mdb" DeleteCommand="DELETE FROM [Users] WHERE [UserId] = ?"
      InsertCommand="INSERT INTO [Users] ([UserId], [UserPassword], [UserRole]) VALUES (?, ?, ?)"
      OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [UserId], [UserPassword], [UserRole] FROM [Users]"
      UpdateCommand="UPDATE [Users] SET [UserPassword] = ?, [UserRole] = ? WHERE [UserId] = ?">
      <DeleteParameters>
         <asp:Parameter Name="original_UserId" Type="String" />
      </DeleteParameters>
      <UpdateParameters>
         <asp:Parameter Name="UserPassword" Type="String" />
         <asp:Parameter Name="UserRole" Type="String" />
         <asp:Parameter Name="original_UserId" Type="String" />
      </UpdateParameters>
      <InsertParameters>
         <asp:Parameter Name="UserId" Type="String" />
         <asp:Parameter Name="UserPassword" Type="String" />
         <asp:Parameter Name="UserRole" Type="String" />
      </InsertParameters>
   </asp:AccessDataSource>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100%">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Users"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100%">
         </td>
      </tr>
      <tr>
         <td style="width: 100%">
         </td>
      </tr>
      <tr>
         <td style="width: 100%">
            <asp:GridView ID="usersGridView" runat="server" AllowPaging="True" AllowSorting="True"
               AutoGenerateColumns="False" DataKeyNames="UserId" DataSourceID="usersAccessDataSource">
               <Columns>
                  <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                  <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" SortExpression="UserId" >
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword" >
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UserRole" HeaderText="UserRole" SortExpression="UserRole" >
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
               </Columns>
               <RowStyle Wrap="False" />
               <EditRowStyle Wrap="False" />
               <SelectedRowStyle Wrap="False" BackColor="#E0E0E0" />
               <AlternatingRowStyle Wrap="False" />
            </asp:GridView>
         </td>
      </tr>
      <tr>
         <td style="width: 100%">
            <asp:Button ID="newUserButton" runat="server" OnClick="newUserButton_Click" Text="Create New User" /></td>
      </tr>
   </table>
   <asp:AccessDataSource ID="usersAccessDataSource" runat="server" ConflictDetection="CompareAllValues"
      DataFile="~/App_Data/betatesting.mdb" DeleteCommand="DELETE FROM [Users] WHERE [UserId] = ? AND [UserPassword] = ? AND [UserRole] = ?"
      InsertCommand="INSERT INTO [Users] ([UserId], [UserPassword], [UserRole]) VALUES (?, ?, ?)"
      OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Users]"
      UpdateCommand="UPDATE [Users] SET [UserPassword] = ?, [UserRole] = ? WHERE [UserId] = ? AND [UserPassword] = ? AND [UserRole] = ?">
      <DeleteParameters>
         <asp:Parameter Name="original_UserId" Type="String" />
         <asp:Parameter Name="original_UserPassword" Type="String" />
         <asp:Parameter Name="original_UserRole" Type="String" />
      </DeleteParameters>
      <UpdateParameters>
         <asp:Parameter Name="UserPassword" Type="String" />
         <asp:Parameter Name="UserRole" Type="String" />
         <asp:Parameter Name="original_UserId" Type="String" />
         <asp:Parameter Name="original_UserPassword" Type="String" />
         <asp:Parameter Name="original_UserRole" Type="String" />
      </UpdateParameters>
      <InsertParameters>
         <asp:Parameter Name="UserId" Type="String" />
         <asp:Parameter Name="UserPassword" Type="String" />
         <asp:Parameter Name="UserRole" Type="String" />
      </InsertParameters>
   </asp:AccessDataSource>
</asp:Content>


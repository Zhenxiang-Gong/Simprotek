<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="BetaTesters.aspx.cs" Inherits="Admin_BetaTesters" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Beta Testers"
               Width="192px"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100px; height: 21px;">
         </td>
      </tr>
      <tr>
         <td style="width: 100px; height: 21px">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            &nbsp;<asp:GridView ID="betaTestersGridView" runat="server" AllowPaging="True" AllowSorting="True"
               AutoGenerateColumns="False" DataKeyNames="TesterId" DataSourceID="betaTestersAccessDataSource"
               OnPageIndexChanged="betaTestersGridView_PageIndexChanged" OnRowEditing="betaTestersGridView_RowEditing"
               OnSelectedIndexChanged="betaTestersGridView_SelectedIndexChanged" PageSize="4">
               <Columns>
                  <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                  <asp:BoundField DataField="TesterId" HeaderText="TesterId" ReadOnly="True" SortExpression="TesterId" />
                  <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                  <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                  <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                  <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                  <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                  <asp:BoundField DataField="StateOrProvince" HeaderText="StateOrProvince" SortExpression="StateOrProvince" />
                  <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" SortExpression="PostalCode" />
                  <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                  <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                  <asp:BoundField DataField="Extension" HeaderText="Extension" SortExpression="Extension" />
                  <asp:BoundField DataField="FaxNumber" HeaderText="FaxNumber" SortExpression="FaxNumber" />
                  <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                  <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" SortExpression="CreationDate" />
               </Columns>
               <SelectedRowStyle BackColor="#E0E0E0" />
            </asp:GridView>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <table style="width: 100%">
               <tr>
                  <td style="width: 60px; height: 328px;">
                     Beta Tester Notes</td>
                  <td style="width: 100px; height: 328px;">
                     <asp:TextBox ID="testerNotesTextBox" runat="server" ReadOnly="True" Rows="20" TextMode="MultiLine"
                        Width="500px"></asp:TextBox></td>
               </tr>
            </table>
            <asp:AccessDataSource ID="betaTestersAccessDataSource" runat="server" ConflictDetection="CompareAllValues"
               DataFile="~/App_Data/betatesting.mdb" DeleteCommand="DELETE FROM [Testers] WHERE [TesterId] = ?"
               InsertCommand="INSERT INTO [Testers] ([TesterId], [CompanyName], [FirstName], [LastName], [Address], [City], [StateOrProvince], [PostalCode], [Country], [PhoneNumber], [Extension], [FaxNumber], [EmailAddress], [Notes], [CreationDate]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
               OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [TesterId], [CompanyName], [FirstName], [LastName], [Address], [City], [StateOrProvince], [PostalCode], [Country], [PhoneNumber], [Extension], [FaxNumber], [EmailAddress], [Notes], [CreationDate] FROM [Testers]"
               UpdateCommand="UPDATE [Testers] SET [CompanyName] = ?, [FirstName] = ?, [LastName] = ?, [Address] = ?, [City] = ?, [StateOrProvince] = ?, [PostalCode] = ?, [Country] = ?, [PhoneNumber] = ?, [Extension] = ?, [FaxNumber] = ?, [EmailAddress] = ?, [Notes] = ?, [CreationDate] = ? WHERE [TesterId] = ?">
               <DeleteParameters>
                  <asp:Parameter Name="original_TesterId" Type="String" />
               </DeleteParameters>
               <UpdateParameters>
                  <asp:Parameter Name="CompanyName" Type="String" />
                  <asp:Parameter Name="FirstName" Type="String" />
                  <asp:Parameter Name="LastName" Type="String" />
                  <asp:Parameter Name="Address" Type="String" />
                  <asp:Parameter Name="City" Type="String" />
                  <asp:Parameter Name="StateOrProvince" Type="String" />
                  <asp:Parameter Name="PostalCode" Type="String" />
                  <asp:Parameter Name="Country" Type="String" />
                  <asp:Parameter Name="PhoneNumber" Type="String" />
                  <asp:Parameter Name="Extension" Type="String" />
                  <asp:Parameter Name="FaxNumber" Type="String" />
                  <asp:Parameter Name="EmailAddress" Type="String" />
                  <asp:Parameter Name="Notes" Type="String" />
                  <asp:Parameter Name="CreationDate" Type="DateTime" />
                  <asp:Parameter Name="original_TesterId" Type="String" />
               </UpdateParameters>
               <InsertParameters>
                  <asp:Parameter Name="TesterId" Type="String" />
                  <asp:Parameter Name="CompanyName" Type="String" />
                  <asp:Parameter Name="FirstName" Type="String" />
                  <asp:Parameter Name="LastName" Type="String" />
                  <asp:Parameter Name="Address" Type="String" />
                  <asp:Parameter Name="City" Type="String" />
                  <asp:Parameter Name="StateOrProvince" Type="String" />
                  <asp:Parameter Name="PostalCode" Type="String" />
                  <asp:Parameter Name="Country" Type="String" />
                  <asp:Parameter Name="PhoneNumber" Type="String" />
                  <asp:Parameter Name="Extension" Type="String" />
                  <asp:Parameter Name="FaxNumber" Type="String" />
                  <asp:Parameter Name="EmailAddress" Type="String" />
                  <asp:Parameter Name="Notes" Type="String" />
                  <asp:Parameter Name="CreationDate" Type="DateTime" />
               </InsertParameters>
            </asp:AccessDataSource>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <asp:Button ID="newBetaTesterButton" runat="server" OnClick="newBetaTesterButton_Click"
               Text="New Beta Tester" /></td>
      </tr>
   </table>
</asp:Content>


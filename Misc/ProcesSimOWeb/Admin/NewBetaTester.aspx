<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="NewBetaTester.aspx.cs" Inherits="Admin_NewBetaTester" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="New Beta Tester"
               Width="196px"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <asp:DetailsView ID="newBetaTesterDetailsView" runat="server" AutoGenerateRows="False"
               DataKeyNames="TesterId" DataSourceID="newBetaTesterAccessDataSource" DefaultMode="Insert"
               Height="50px" OnItemInserted="newBetaTesterDetailsView_ItemInserted" OnItemInserting="newBetaTesterDetailsView_ItemInserting"
               Width="534px">
               <Fields>
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
                  <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                  <asp:CommandField ShowInsertButton="True" />
               </Fields>
            </asp:DetailsView>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <asp:Label ID="newTesterMessageLabel" runat="server" Width="250px" ForeColor="Red"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
      </tr>
   </table>
   <asp:AccessDataSource ID="newBetaTesterAccessDataSource" runat="server" ConflictDetection="CompareAllValues"
      DataFile="~/App_Data/betatesting.mdb" DeleteCommand="DELETE FROM [Testers] WHERE [TesterId] = ? AND [CompanyName] = ? AND [FirstName] = ? AND [LastName] = ? AND [Address] = ? AND [City] = ? AND [StateOrProvince] = ? AND [PostalCode] = ? AND [Country] = ? AND [PhoneNumber] = ? AND [Extension] = ? AND [FaxNumber] = ? AND [EmailAddress] = ? AND [Notes] = ? AND [CreationDate] = ?"
      InsertCommand="INSERT INTO [Testers] ([TesterId], [CompanyName], [FirstName], [LastName], [Address], [City], [StateOrProvince], [PostalCode], [Country], [PhoneNumber], [Extension], [FaxNumber], [EmailAddress], [Notes], [CreationDate]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
      OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [TesterId], [CompanyName], [FirstName], [LastName], [Address], [City], [StateOrProvince], [PostalCode], [Country], [PhoneNumber], [Extension], [FaxNumber], [EmailAddress], [Notes], [CreationDate] FROM [Testers]"
      UpdateCommand="UPDATE [Testers] SET [CompanyName] = ?, [FirstName] = ?, [LastName] = ?, [Address] = ?, [City] = ?, [StateOrProvince] = ?, [PostalCode] = ?, [Country] = ?, [PhoneNumber] = ?, [Extension] = ?, [FaxNumber] = ?, [EmailAddress] = ?, [Notes] = ?, [CreationDate] = ? WHERE [TesterId] = ? AND [CompanyName] = ? AND [FirstName] = ? AND [LastName] = ? AND [Address] = ? AND [City] = ? AND [StateOrProvince] = ? AND [PostalCode] = ? AND [Country] = ? AND [PhoneNumber] = ? AND [Extension] = ? AND [FaxNumber] = ? AND [EmailAddress] = ? AND [Notes] = ? AND [CreationDate] = ?" OnInserted="newBetaTesterAccessDataSource_Inserted">
      <DeleteParameters>
         <asp:Parameter Name="original_TesterId" Type="String" />
         <asp:Parameter Name="original_CompanyName" Type="String" />
         <asp:Parameter Name="original_FirstName" Type="String" />
         <asp:Parameter Name="original_LastName" Type="String" />
         <asp:Parameter Name="original_Address" Type="String" />
         <asp:Parameter Name="original_City" Type="String" />
         <asp:Parameter Name="original_StateOrProvince" Type="String" />
         <asp:Parameter Name="original_PostalCode" Type="String" />
         <asp:Parameter Name="original_Country" Type="String" />
         <asp:Parameter Name="original_PhoneNumber" Type="String" />
         <asp:Parameter Name="original_Extension" Type="String" />
         <asp:Parameter Name="original_FaxNumber" Type="String" />
         <asp:Parameter Name="original_EmailAddress" Type="String" />
         <asp:Parameter Name="original_Notes" Type="String" />
         <asp:Parameter Name="original_CreationDate" Type="DateTime" />
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
         <asp:Parameter Name="original_CompanyName" Type="String" />
         <asp:Parameter Name="original_FirstName" Type="String" />
         <asp:Parameter Name="original_LastName" Type="String" />
         <asp:Parameter Name="original_Address" Type="String" />
         <asp:Parameter Name="original_City" Type="String" />
         <asp:Parameter Name="original_StateOrProvince" Type="String" />
         <asp:Parameter Name="original_PostalCode" Type="String" />
         <asp:Parameter Name="original_Country" Type="String" />
         <asp:Parameter Name="original_PhoneNumber" Type="String" />
         <asp:Parameter Name="original_Extension" Type="String" />
         <asp:Parameter Name="original_FaxNumber" Type="String" />
         <asp:Parameter Name="original_EmailAddress" Type="String" />
         <asp:Parameter Name="original_Notes" Type="String" />
         <asp:Parameter Name="original_CreationDate" Type="DateTime" />
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
         <asp:SessionParameter Name="CreationDate" SessionField="TesterCreationDate"
            Type="DateTime" />
      </InsertParameters>
   </asp:AccessDataSource>
</asp:Content>


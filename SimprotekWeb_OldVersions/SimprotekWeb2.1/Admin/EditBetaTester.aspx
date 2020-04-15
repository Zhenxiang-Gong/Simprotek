<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EditBetaTester.aspx.cs" Inherits="Admin_EditBetaTester" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Edit Beta Tester"
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
         <td style="width: 100%">
            <table style="width: 100%">
               <tr>
                  <td style="width: 478%">
                     &nbsp;<table style="width: 100%">
                        <tr>
                           <td style="width: 100px">
                              TesterId</td>
                           <td style="width: 100px">
                              <asp:Label ID="testerIdLabel" runat="server" Width="268px"></asp:Label></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              CompanyName</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="companyNameTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              FirstName</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="firstNameTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              LastName</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="lastNameTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              Address</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="addressTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              City</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="cityTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              StateOrProvince</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="stateOrProvinceTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              PostalCode</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="postalCodeTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              Country</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="countryTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              PhoneNumber</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="phoneNumberTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              Extension</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="extensionTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              FaxNumber</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="faxNumberTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              EmailAddress</td>
                           <td style="width: 100px">
                              <asp:TextBox ID="emailAddressTextBox" runat="server" Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                              CreationDate</td>
                           <td style="width: 100px">
                              <asp:Label ID="creationDateLabel" runat="server" Width="268px"></asp:Label></td>
                        </tr>
                     </table>
                  </td>
                  <td style="width: 100%">
                     <table style="width: 100%">
                        <tr>
                           <td style="width: 100px">
                              Notes</td>
                        </tr>
                        <tr>
                           <td style="width: 100px">
                     <asp:TextBox ID="testerNotesTextBox" runat="server" Rows="20" TextMode="MultiLine"
                        Width="500px"></asp:TextBox></td>
                        </tr>
                     </table>
                  </td>
               </tr>
            </table>
            <asp:Button ID="editTesterUpdateButton" runat="server" OnClick="editTesterUpdateButton_Click"
               Text="Update" /><asp:Button ID="editTesterCancelButton" runat="server" OnClick="editTesterCancelButton_Click"
                  Text="Cancel" /></td>
      </tr>
      <tr>
         <td style="width: 100px">
            <asp:Label ID="editTesterMessageLabel" runat="server" Width="250px" ForeColor="Red"></asp:Label></td>
      </tr>
   </table>
</asp:Content>


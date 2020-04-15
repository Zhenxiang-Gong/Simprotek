<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Defects.aspx.cs" Inherits="Admin_Defects" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentPlaceHolder" Runat="Server">
   <table style="width: 100%; height: 100%">
      <tr>
         <td style="width: 100px">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="Defects"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100px">
            &nbsp; &nbsp;&nbsp;
            <asp:GridView ID="adminDefectsGridView" runat="server" AllowPaging="True" AllowSorting="True"
               AutoGenerateColumns="False" DataKeyNames="DefectId" DataSourceID="adminDefectsAccessDataSource"
               EmptyDataText="There are no data records to display." OnSelectedIndexChanged="adminDefectsGridView_SelectedIndexChanged"
               PageSize="4" OnRowEditing="adminDefectsGridView_RowEditing" Width="100%" OnPageIndexChanged="adminDefectsGridView_PageIndexChanged">
               <Columns>
                  <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                  <asp:BoundField DataField="DefectId" HeaderText="DefectId" InsertVisible="False"
                     ReadOnly="True" SortExpression="DefectId">
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="TesterId" HeaderText="TesterId" SortExpression="TesterId">
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DefectTitle" HeaderText="DefectTitle" SortExpression="DefectTitle">
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:CheckBoxField DataField="Fixed" HeaderText="Fixed" SortExpression="Fixed">
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:CheckBoxField>
                  <asp:CheckBoxField DataField="Postponed" HeaderText="Postponed" SortExpression="Postponed">
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:CheckBoxField>
                  <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated">
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DateFixed" HeaderText="DateFixed" SortExpression="DateFixed">
                     <ItemStyle Wrap="False" />
                  </asp:BoundField>
               </Columns>
               <SelectedRowStyle BackColor="#E0E0E0" />
            </asp:GridView>
            <asp:AccessDataSource ID="adminDefectsAccessDataSource" runat="server" ConflictDetection="CompareAllValues"
               DataFile="~/App_Data/betatesting.mdb" DeleteCommand="DELETE FROM [Defects] WHERE [DefectId] = ?"
               InsertCommand="INSERT INTO [Defects] ([DefectId], [TesterId], [DefectTitle], [DefectDescription], [Notes], [Fixed], [Postponed], [DateCreated], [DateFixed]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"
               OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [DefectId], [TesterId], [DefectTitle], [DefectDescription], [Notes], [Fixed], [Postponed], [DateCreated], [DateFixed] FROM [Defects]"
               UpdateCommand="UPDATE [Defects] SET [TesterId] = ?, [DefectTitle] = ?, [DefectDescription] = ?, [Notes] = ?, [Fixed] = ?, [Postponed] = ?, [DateCreated] = ?, [DateFixed] = ? WHERE [DefectId] = ?">
               <DeleteParameters>
                  <asp:Parameter Name="original_DefectId" Type="Int32" />
               </DeleteParameters>
               <UpdateParameters>
                  <asp:Parameter Name="TesterId" Type="String" />
                  <asp:Parameter Name="DefectTitle" Type="String" />
                  <asp:Parameter Name="DefectDescription" Type="String" />
                  <asp:Parameter Name="Notes" Type="String" />
                  <asp:Parameter Name="Fixed" Type="Boolean" />
                  <asp:Parameter Name="Postponed" Type="Boolean" />
                  <asp:Parameter Name="DateCreated" Type="DateTime" />
                  <asp:Parameter Name="DateFixed" Type="DateTime" />
                  <asp:Parameter Name="original_DefectId" Type="Int32" />
               </UpdateParameters>
               <InsertParameters>
                  <asp:Parameter Name="DefectId" Type="Int32" />
                  <asp:Parameter Name="TesterId" Type="String" />
                  <asp:Parameter Name="DefectTitle" Type="String" />
                  <asp:Parameter Name="DefectDescription" Type="String" />
                  <asp:Parameter Name="Notes" Type="String" />
                  <asp:Parameter Name="Fixed" Type="Boolean" />
                  <asp:Parameter Name="Postponed" Type="Boolean" />
                  <asp:Parameter Name="DateCreated" Type="DateTime" />
                  <asp:Parameter Name="DateFixed" Type="DateTime" />
               </InsertParameters>
            </asp:AccessDataSource>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            <table style="width: 100%">
               <tr>
                  <td style="width: 100px">
                     Defect Description</td>
                  <td style="width: 100px">
                     Defect Notes</td>
               </tr>
               <tr>
                  <td style="width: 100px; height: 328px;">
                     <asp:TextBox ID="defectDescriptionTextBox" runat="server" ReadOnly="True" Rows="20"
                        TextMode="MultiLine" Width="500px" Wrap="False"></asp:TextBox></td>
                  <td style="width: 100px; height: 328px;">
                     <asp:TextBox ID="defectNotesTextBox" runat="server" ReadOnly="True" Rows="20" TextMode="MultiLine"
                        Width="500px" Wrap="False"></asp:TextBox></td>
               </tr>
            </table>
         </td>
      </tr>
   </table>
</asp:Content>


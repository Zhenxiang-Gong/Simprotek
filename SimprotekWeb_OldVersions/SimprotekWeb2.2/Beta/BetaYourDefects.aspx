<%@ Page Language="C#" MasterPageFile="~/Beta/Beta.master" AutoEventWireup="true" CodeFile="BetaYourDefects.aspx.cs" Inherits="BetaYourDefects" Title="Untitled Page" %>
<asp:Content ID="betaYourDefectsContent" ContentPlaceHolderID="betaContentPlaceHolder" Runat="Server">
   <table style="width: 100%; height: 100%">
      <tr>
         <td style="width: 100px">
         </td>
         <td align="center" style="text-align: left" width="80%">
            <asp:Label ID="titleLabel" runat="server" Font-Size="X-Large" Text="My Reported Defects"
               Width="257px"></asp:Label></td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
         <td align="center" style="text-align: center" width="80%">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
         </td>
         <td align="center" style="text-align: center" width="80%">
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            Defects</td>
         <td align="center" style="text-align: center;" width="80%">
            <asp:GridView ID="yourDefectsGridView" runat="server" AutoGenerateColumns="False"
               DataKeyNames="DefectId" DataSourceID="myDefectsAccessDataSource" OnSelectedIndexChanged="yourDefectsGridView_SelectedIndexChanged"
               Width="100%" AllowPaging="True" PageSize="6" HorizontalAlign="Center">
               <Columns>
                  <asp:CommandField ShowSelectButton="True" />
                  <asp:BoundField DataField="DefectId" HeaderText="DefectId" InsertVisible="False"
                     ReadOnly="True" SortExpression="DefectId" />
                  <asp:BoundField DataField="DefectTitle" HeaderText="DefectTitle" SortExpression="DefectTitle" />
                  <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
               </Columns>
            </asp:GridView>
         </td>
      </tr>
      <tr>
         <td style="width: 100px">
            &nbsp;Defect Description</td>
         <td style="width: 100px">
            <asp:TextBox ID="defectDescriptionTextBox" runat="server" Rows="20" TextMode="MultiLine"
               Width="500px" ReadOnly="True"></asp:TextBox></td>
      </tr>
   </table>
   <asp:AccessDataSource ID="myDefectsAccessDataSource" runat="server" DataFile="~/App_Data/betatesting.mdb"
      SelectCommand="SELECT [DefectId], [DefectTitle], [DateCreated] FROM [Defects] WHERE ([TesterId] = ?)">
      <SelectParameters>
         <asp:SessionParameter Name="TesterId" SessionField="UserId" Type="String" />
      </SelectParameters>
   </asp:AccessDataSource>
</asp:Content>


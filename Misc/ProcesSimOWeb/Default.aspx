<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
      <tr>
         <td nowrap="nowrap" style="width: 50%; height: 19px">
            <asp:Label ID="Label5" runat="server" Text=" Mission Statement:" Width="366px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td nowrap="nowrap" style="width: 50%; height: 19px">
            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Black" Text=" What's New:"
               Width="366px"></asp:Label></td>
      </tr>
      <tr>
         <td nowrap="nowrap" style="width: 50%">
            <asp:Label ID="Label7" runat="server" Text=" " Width="366px"></asp:Label></td>
         <td nowrap="nowrap" style="width: 50%">
            <asp:Label ID="Label8" runat="server" Text=" " Width="366px"></asp:Label></td>
      </tr>
      <tr>
         <td nowrap="nowrap" style="width: 50%">
            TO DO: write the mission statement</td>
         <td nowrap="nowrap" style="width: 50%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
               <tr>
                  <td style="width: 100%">
                     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                           <td style="width: 10%">
                     <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/appico.bmp" /></td>
                           <td style="width: 90%">
                     <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="X-Large" Text="ProSimO 1.0"
                        Width="165px"></asp:Label></td>
                        </tr>
                     </table>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Process Simulation and Optimization"
               Width="302px"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 100%">
                  </td>
               </tr>
               <tr>
                  <td style="width: 100%">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Beta 1 now available!"
               Width="282px" ForeColor="Orange"></asp:Label></td>
               </tr>
               <tr>
                  <td style="width: 100%">
            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="DimGray" PostBackUrl="~/Company.aspx"
               Width="305px">Contact us if you would like to test the software</asp:LinkButton></td>
               </tr>
               <tr>
                  <td style="width: 100%">
                  </td>
               </tr>
            </table>
         </td>
      </tr>
   </table>
</asp:Content>


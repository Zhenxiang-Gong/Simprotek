<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" Title="Untitled Page" %>
<asp:Content ID="ProductsContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
   <table style="width: 100%">
      <tr>
         <td style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
               <tr>
                  <td style="width: 50%; height: 89px;">
                     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                           <td style="width: 100%; height: 51px">
                              <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                 <tr>
                                    <td style="width: 10%">
                     <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/appico.bmp" /></td>
                                    <td style="width: 90%">
                     <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="X-Large" Text="ProSimO 1.0"
                        Width="165px"></asp:Label></td>
                                 </tr>
                              </table>
            <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Process Simulation and Optimization"
               Width="264px"></asp:Label></td>
                        </tr>
                        <tr>
                           <td style="width: 100%; height: 19px;">
                              <asp:Label ID="Label1" runat="server" Text=" " Width="405px"></asp:Label></td>
                        </tr>
                     </table>
                  </td>
                  <td style="width: 50%; height: 89px;">
                     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                           <td style="width: 100%">
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Beta 1 now available!"
               Width="273px" ForeColor="Orange"></asp:Label></td>
                        </tr>
                        <tr>
                           <td style="width: 100%; height: 19px">
            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="DimGray" PostBackUrl="~/Company.aspx"
               Width="320px">Contact us if you would like to test the software</asp:LinkButton></td>
                        </tr>
                     </table>
                  </td>
               </tr>
            </table>
         </td>
      </tr>
      <tr>
         <td style="width: 100%">
            TO DO: Description of the product</td>
      </tr>
   </table>
</asp:Content>


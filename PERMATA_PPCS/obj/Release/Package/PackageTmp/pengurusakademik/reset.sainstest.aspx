<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengurusakademik/main.Master" CodeBehind="reset.sainstest.aspx.vb" Inherits="permatapintar.reset_sainstest" %>
<%@ Register src="../commoncontrol/reset_sainstest.ascx" tagname="reset_sainstest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Kemaskini > Science Interest Inventory" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
     <uc1:reset_sainstest ID="reset_sainstest1" runat="server" />
</asp:Content>

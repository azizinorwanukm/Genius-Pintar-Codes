<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengurusakademik/main.Master" CodeBehind="reset.eqtest.aspx.vb" Inherits="permatapintar.reset_eqtest" %>
<%@ Register src="../commoncontrol/reset_eqtest.ascx" tagname="reset_eqtest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Kemaskini > EQ Test" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
     <uc1:reset_eqtest ID="reset_eqtest1" runat="server" />
</asp:Content>

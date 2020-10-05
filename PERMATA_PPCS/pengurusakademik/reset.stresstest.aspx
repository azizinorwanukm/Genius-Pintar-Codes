<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengurusakademik/main.Master" CodeBehind="reset.stresstest.aspx.vb" Inherits="permatapintar.reset_stresstest" %>

<%@ Register Src="../commoncontrol/reset_stresstest.ascx" TagName="reset_stresstest" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Kemaskini > Stress Test" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:reset_stresstest ID="reset_stresstest1" runat="server" />
    &nbsp;
</asp:Content>

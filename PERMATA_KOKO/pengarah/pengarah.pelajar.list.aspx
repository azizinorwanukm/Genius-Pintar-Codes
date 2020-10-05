<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah/pengarah.Master" CodeBehind="pengarah.pelajar.list.aspx.vb" Inherits="permatapintar.pengarah_pelajar_list" %>

<%@ Register Src="../commoncontrol/pelajar_list.ascx" TagName="pelajar_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Carian>Carian Pelajar
            </td>
        </tr>
    </table>
    <uc1:pelajar_list ID="pelajar_list1" runat="server" />
</asp:Content>

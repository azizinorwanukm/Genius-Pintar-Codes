<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.pelajar.view.aspx.vb" Inherits="permatapintar.instruktor_pelajar_view" %>

<%@ Register src="../commoncontrol/studentprofile_view.ascx" tagname="studentprofile_view" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Senarai Pelajar>Paparan Profil Pelajar
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
</asp:Content>

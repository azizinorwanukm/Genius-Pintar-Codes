<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.profil.view.aspx.vb" Inherits="permatapintar.pelajar_profil_view" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pelajar>Profil
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;
</asp:Content>

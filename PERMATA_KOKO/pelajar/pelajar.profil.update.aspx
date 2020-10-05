<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.profil.update.aspx.vb" Inherits="permatapintar.pelajar_profil_update" %>

<%@ Register Src="../commoncontrol/studentprofile_update_mykad.ascx" TagName="studentprofile_update_mykad" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pelajar>Kemaskini Profil
            </td>
        </tr>
    </table>
    <uc1:studentprofile_update_mykad ID="studentprofile_update_mykad1" runat="server" />
    &nbsp;
</asp:Content>

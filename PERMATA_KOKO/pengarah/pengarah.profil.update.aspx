<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah/pengarah.Master" CodeBehind="pengarah.profil.update.aspx.vb" Inherits="permatapintar.pengarah_profil_update" %>

<%@ Register Src="../commoncontrol/pengarah_update.ascx" TagName="pengarah_update" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Kemaskini Profil
            </td>
        </tr>
    </table>

    <uc1:pengarah_update ID="pengarah_update1" runat="server" />
</asp:Content>

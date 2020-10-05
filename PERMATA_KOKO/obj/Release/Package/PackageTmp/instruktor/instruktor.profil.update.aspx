<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.profil.update.aspx.vb" Inherits="permatapintar.instruktor_profil_update" %>

<%@ Register src="../commoncontrol/instruktor_update.ascx" tagname="instruktor_update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Kemaskini Profil
            </td>
        </tr>
    </table>
    <uc1:instruktor_update ID="instruktor_update1" runat="server" />
</asp:Content>

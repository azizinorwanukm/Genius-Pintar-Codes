<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.password.update.aspx.vb" Inherits="permatapintar.instruktor_password_update" %>
<%@ Register src="../commoncontrol/instruktor_update_pwd.ascx" tagname="instruktor_update_pwd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Tukar Kata Laluan
            </td>
        </tr>
    </table>
    
    <uc1:instruktor_update_pwd ID="instruktor_update_pwd1" runat="server" />
</asp:Content>

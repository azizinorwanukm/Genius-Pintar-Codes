<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.uniform.jawatan.update.aspx.vb" Inherits="permatapintar.instruktor_uniform_jawatan_update" %>

<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<%@ Register src="../commoncontrol/koko_pelajar_jawatan_uniform.ascx" tagname="koko_pelajar_jawatan_uniform" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Jawatan Pelajar>Kemaskini
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header ID="instruktor_view_header1" runat="server" />
    &nbsp;
    <uc1:koko_pelajar_jawatan_uniform ID="koko_pelajar_jawatan_uniform1" runat="server" />
    &nbsp; 
</asp:Content>

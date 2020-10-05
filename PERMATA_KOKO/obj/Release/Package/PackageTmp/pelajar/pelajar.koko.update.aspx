<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.koko.update.aspx.vb" Inherits="permatapintar.pelajar_koko_update" %>

<%@ Register Src="../commoncontrol/studentprofile_view_header.ascx" TagName="studentprofile_view_header" TagPrefix="uc1" %>
<%@ Register src="../commoncontrol/koko_update.ascx" tagname="koko_update" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pelajar>Kemaskini Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view_header ID="studentprofile_view_header1" runat="server" />
    &nbsp;
    <uc2:koko_update ID="koko_update1" runat="server" />
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.koko.pencapaian.update.aspx.vb" Inherits="permatapintar.pelajar_koko_pencapaian_update" %>

<%@ Register Src="../commoncontrol/studentprofile_view_header.ascx" TagName="studentprofile_view_header" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/koko_list.ascx" TagName="koko_list" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/koko_pencapaian_update.ascx" TagName="koko_pencapaian_update" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pelajar>Kemaskini Pencapaian
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view_header ID="studentprofile_view_header1" runat="server" />
     &nbsp;
    <uc2:koko_list ID="koko_list1" runat="server" />
    <uc3:koko_pencapaian_update ID="koko_pencapaian_update1" runat="server" />

</asp:Content>

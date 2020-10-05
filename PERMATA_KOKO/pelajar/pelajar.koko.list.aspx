<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.koko.list.aspx.vb" Inherits="permatapintar.pelajar_koko_list" %>

<%@ Register Src="../commoncontrol/studentprofile_view_header.ascx" TagName="studentprofile_view_header" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/koko_list.ascx" TagName="koko_list" TagPrefix="uc2" %>
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
    &nbsp;

</asp:Content>

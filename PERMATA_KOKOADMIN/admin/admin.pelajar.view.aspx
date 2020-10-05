<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pelajar.view.aspx.vb" Inherits="permatapintar.admin_pelajar_view" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/koko_list.ascx" TagName="koko_list" TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Paparan Maklumat Pelajar
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc3:koko_list ID="koko_list1" runat="server" />
</asp:Content>

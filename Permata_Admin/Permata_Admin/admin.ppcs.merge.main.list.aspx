<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.merge.main.list.aspx.vb" Inherits="permatapintar.admin_ppcs_merge_main_list" %>

<%@ Register Src="commoncontrol/ppcs_merge_main_select.ascx" TagName="ppcs_merge_main_select"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_merge_header.ascx" TagName="studentprofile_merge_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Merge Account" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_merge_main_select ID="ppcs_merge_main_select1" runat="server" />
    &nbsp;
</asp:Content>

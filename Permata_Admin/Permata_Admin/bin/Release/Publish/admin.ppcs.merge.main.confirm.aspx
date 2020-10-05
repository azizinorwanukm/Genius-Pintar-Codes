<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.merge.main.confirm.aspx.vb" Inherits="permatapintar.admin_ppcs_merge_main_confirm" %>

<%@ Register Src="commoncontrol/ppcs_merge_main_confirm.ascx" TagName="ppcs_merge_main_confirm"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_merge_header.ascx" TagName="studentprofile_merge_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Merge Account>Merge With" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:studentprofile_merge_header ID="studentprofile_merge_header1" runat="server" />
    &nbsp;<uc1:ppcs_merge_main_confirm ID="ppcs_merge_main_confirm1" runat="server" />
</asp:Content>

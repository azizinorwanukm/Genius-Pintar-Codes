<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.progress.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_progress" %>
<%@ Register src="commoncontrol/exam_progress.ascx" tagname="exam_progress" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian PCIS>Status Ujian" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:exam_progress ID="exam_progress1" runat="server" />
&nbsp;
</asp:Content>

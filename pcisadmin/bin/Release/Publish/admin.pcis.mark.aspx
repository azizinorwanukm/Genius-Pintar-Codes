<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.mark.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_mark" %>
<%@ Register src="commoncontrol/exam_mark.ascx" tagname="exam_mark" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian PCIS>Markah Ujian" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:exam_mark ID="exam_mark1" runat="server" />
&nbsp;
</asp:Content>

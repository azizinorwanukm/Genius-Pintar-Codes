<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.studentrace.list.aspx.vb" Inherits="permatapintar.admin_studentprofile_studentrace_list" %>

<%@ Register Src="commoncontrol/studentprofile_studentrace_list.ascx" TagName="studentprofile_studentrace_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Bangsa>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_studentrace_list ID="studentprofile_studentrace_list1" runat="server" />
    &nbsp;
</asp:Content>

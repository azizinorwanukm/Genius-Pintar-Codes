<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_admin.markahlist.aspx.vb" Inherits="permatapintar.ukm3_admin_markahlist" %>

<%@ Register Src="~/commoncontrol/student_exam.ascx" TagPrefix="uc1" TagName="student_exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Markah>Ubah Markah"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:student_exam runat="server" id="student_exam" />
</asp:Content>

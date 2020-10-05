<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.studentprofile.studentrace.list.aspx.vb" Inherits="permatapintar.ukm2_studentprofile_studentrace_list1" %>

<%@ Register src="../commoncontrol/ukm2_studentprofile_studentrace_list.ascx" tagname="ukm2_studentprofile_studentrace_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Bangsa>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    &nbsp;<uc1:ukm2_studentprofile_studentrace_list ID="ukm2_studentprofile_studentrace_list1" runat="server" />
&nbsp;
</asp:Content>
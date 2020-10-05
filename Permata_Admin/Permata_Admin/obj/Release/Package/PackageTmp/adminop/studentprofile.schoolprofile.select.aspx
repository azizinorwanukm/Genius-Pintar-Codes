<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="studentprofile.schoolprofile.select.aspx.vb" Inherits="permatapintar.studentprofile_schoolprofile_select1" %>

<%@ Register src="../commoncontrol/studentprofile_schoolprofile_select.ascx" tagname="studentprofile_schoolprofile_select" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="Label1" runat="server" text="Maklumat Pelajar>Pindah Sekolah>Pilih Sekolah"
                    cssclass="lblBreadcrum"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_schoolprofile_select ID="studentprofile_schoolprofile_select1" runat="server" />
</asp:Content>
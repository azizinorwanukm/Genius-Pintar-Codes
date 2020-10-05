<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.create.aspx.vb" Inherits="permatapintar.admin_studentprofile_create" %>

<%@ Register src="commoncontrol/studentprofile_create.ascx" tagname="studentprofile_create" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="Label1" runat="server" text="Maklumat Pelajar>Daftar Pelajar Baru"
                    cssclass="lblBreadcrum"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_create ID="studentprofile_create1" runat="server" />
</asp:Content>

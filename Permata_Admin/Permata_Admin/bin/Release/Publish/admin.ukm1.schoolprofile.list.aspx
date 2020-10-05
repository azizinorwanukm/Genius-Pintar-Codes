<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm1.schoolprofile.list.aspx.vb" Inherits="permatapintar.admin_ukm1_schoolprofile_list" %>

<%@ Register src="commoncontrol/ukm1_schoolprofile_list.ascx" tagname="ukm1_schoolprofile_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="Label1" runat="server" text="Ujian UKM1>Senarai Sekolah" cssclass="lblBreadcrum"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schoolprofile_list ID="ukm1_schoolprofile_list1" runat="server" />
</asp:Content>

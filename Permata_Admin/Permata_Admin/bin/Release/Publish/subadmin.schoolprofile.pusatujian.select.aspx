<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.schoolprofile.pusatujian.select.aspx.vb" Inherits="permatapintar.subadmin_schoolprofile_pusatujian_select" %>

<%@ Register Src="commoncontrol/schoolprofile_pusatujian_select.ascx" TagName="schoolprofile_pusatujian_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Daftar Pusat Ujian"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_pusatujian_select ID="schoolprofile_pusatujian_select1"
        runat="server" />
</asp:Content>

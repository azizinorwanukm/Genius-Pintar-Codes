<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.schoolprofile.pusatujian.confirm.aspx.vb" Inherits="permatapintar.admin_schoolprofile_pusatujian_confirm" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Daftar Pusat Ujian Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td class="fbtd_left">Jumlah Lab:
            </td>
            <td>
                <asp:TextBox ID="txtJumLab" runat="server" Width="120px" MaxLength="5"></asp:TextBox>*
            </td>
        </tr>
        <tr>
            <td>Jumlah Komputer:
            </td>
            <td>
                <asp:TextBox ID="txtJumKomp" runat="server" Width="120px" MaxLength="5"></asp:TextBox>*
            </td>
        </tr>
        <tr>
            <td>Tahun Ujian:
            </td>
            <td>
                <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnConfirm" runat="server" Text="Daftar Pusat Ujian" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

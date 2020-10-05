<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.schoolprofile.pusatujian.confirm.aspx.vb" Inherits="permatapintar.subadmin_schoolprofile_pusatujian_confirm" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td class="fbtd_left">Jumlah Lab:
            </td>
            <td>
                <asp:TextBox ID="txtJumLab" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Jumlah Komputer:
            </td>
            <td>
                <asp:TextBox ID="txtJumKomp" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
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
            <td class="fbform_sap" colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnConfirm" runat="server" Text="Daftar Pusat Ujian" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>

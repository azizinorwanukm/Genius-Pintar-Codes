<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.schoolprofile.pusatujian.confirm.aspx.vb" Inherits="permatapintar.jpn_schoolprofile_pusatujian_confirm" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Pusat Ujian UKM2>Daftar Pusat Ujian>Sahkan Pusat Ujian Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td class="fbtd_left">Tahun:
            </td>
            <td>
                <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 15%;">Jumlah Lab:
            </td>
            <td>
                <asp:TextBox ID="txtJumLab" runat="server" Width="200px" MaxLength="5"></asp:TextBox>*
            </td>
        </tr>
        <tr>
            <td>Jumlah Komputer:
            </td>
            <td>
                <asp:TextBox ID="txtJumKomp" runat="server" Width="200px" MaxLength="5"></asp:TextBox>*
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="2"></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnConfirm" runat="server" Text="Daftar Pusat Ujian" CssClass="fbbutton" />&nbsp;|<asp:LinkButton ID="lnkBack" runat="server">Kembali</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>

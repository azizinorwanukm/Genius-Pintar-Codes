<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="keterujaan.view.complete.aspx.vb" Inherits="permatapintar.keterujaan_view_complete" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/keterujaan.view.01.ascx" TagName="keterujaan"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/keterujaan.view.02.ascx" TagName="keterujaan"
    TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/keterujaan.view.03.ascx" TagName="keterujaan"
    TagPrefix="uc4" %>
<%@ Register Src="../commoncontrol/keterujaan.view.04.ascx" TagName="keterujaan"
    TagPrefix="uc5" %>
<%@ Register Src="../commoncontrol/keterujaan.view.05.ascx" TagName="keterujaan"
    TagPrefix="uc6" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Penilaian Kendiri (View only)
            </td>
        </tr>
    </table>
    <uc1:ukm2 ID="ukm21" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <b>Mukasurat 1</b>
    <uc2:keterujaan ID="keterujaan1" runat="server" />
    <b>Mukasurat 2</b>
    <uc3:keterujaan ID="keterujaan2" runat="server" />
    <b>Mukasurat 3</b>
    <uc4:keterujaan ID="keterujaan3" runat="server" />
    <b>Mukasurat 4</b>
    <uc5:keterujaan ID="Keterujaan4" runat="server" />
    <b>Mukasurat 5</b>
    <uc6:keterujaan ID="Keterujaan5" runat="server" />
    
</asp:Content>
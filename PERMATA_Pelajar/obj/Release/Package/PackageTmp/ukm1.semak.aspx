<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master"
    CodeBehind="ukm1.semak.aspx.vb" Inherits="permatapintar.ukm1_semak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Semak status Ujian UKM1 anda.
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                Tahun Ujian:
            </td>
            <td>
                <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="100px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Nota: Untuk yang menghabiskan Ujian UKM1 sahaja. Status=DONE.
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
    <asp:Label ID="lblTotalMin" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDOB_Year" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
    <asp:Label ID="lblDuration" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblExamStart" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblExamEnd" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

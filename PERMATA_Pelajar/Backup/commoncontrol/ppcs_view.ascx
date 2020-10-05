<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_view.ascx.vb"
    Inherits="permatapintar.ppcs_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat PPCS
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Black" Visible="true">Kemaskini</asp:LinkButton>
        </td>
    </tr>
   <%-- <tr>
        <td>
            Tahun Ujian:
        </td>
        <td>
            :<asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>--%>
    <tr>
        <td>
            Sessi PPCS:
        </td>
        <td>
            :<asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="btnLoad" runat="server" Text="Load Info" CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Kursus
        </td>
        <td>
            :<asp:Label ID="lblPPCSCourse" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Kelas
        </td>
        <td>
            :<asp:Label ID="lblPPCSClass" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Tempat
        </td>
        <td>
            :<asp:Label ID="lblTempat" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="width: 15%;">
            Status
        </td>
        <td>
            :<asp:Label ID="lblPPCSStatus" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td>
            Nama Asrama
        </td>
        <td>
            :<asp:Label ID="lblNamaAsrama" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            No Bilik
        </td>
        <td>
            :<asp:Label ID="lblNoBilik" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Saiz Baju
        </td>
        <td>
            :<asp:Label ID="lblSaizBaju" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Sakit/Alahan
        </td>
        <td>
            :<asp:Label ID="lblSakitAlahan" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>

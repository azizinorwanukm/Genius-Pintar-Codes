<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_update.ascx.vb"
    Inherits="permatapintar.ppcs_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini Maklumat PPCS
        </td>
    </tr>
    
    <tr>
        <td class="fbtd_left">
            Sessi PPCS:
        </td>
        <td>
            :<asp:Label ID="lblPPCSDate" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Status
        </td>
        <td>
            :<asp:Label ID="lblPPCSStatus" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            <asp:DropDownList ID="ddlPPCSStatus" runat="server" Width="200px" Visible="false">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td >
            Kursus
        </td>
        <td>
            :<asp:DropDownList ID="ddlPPCSCourse" runat="server" Width="100px" AutoPostBack="true"
                Visible="true">
            </asp:DropDownList>
            <asp:Label ID="lblPPCSCourse" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            <asp:TextBox ID="txtPPCSCourse" runat="server" Width="100px" MaxLength="150" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >
            Kelas
        </td>
        <td>
            :<asp:DropDownList ID="ddlPPCSClass" runat="server" Width="100px" AutoPostBack="true"
                Visible="true">
            </asp:DropDownList>
            <asp:Label ID="lblPPCSClass" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            <asp:TextBox ID="txtPPCSClass" runat="server" Width="100px" MaxLength="150" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Tempat
        </td>
        <td>
            :<asp:Label ID="lblTempat" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama Asrama
        </td>
        <td>
            :<asp:TextBox ID="txtNamaAsrama" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >
            No Bilik
        </td>
        <td>
            :<asp:TextBox ID="txtNoBilik" runat="server" Width="50px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >
            Saiz Baju
        </td>
        <td>
            :<asp:TextBox ID="txtSaizBaju" runat="server" Width="50px" MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >
            Sakit/Alahan
        </td>
        <td>
            :<asp:TextBox ID="txtSakitAlahan" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:LinkButton
                ID="lnkStudentProfileView" runat="server">View Student Profile</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            Create&nbsp;>&nbsp;
            <asp:LinkButton ID="lnkHarian" runat="server">Laporan Harian</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkMingguan" runat="server">Laporan Mingguan</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkAkhir" runat="server">Laporan Akhir</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi. Perlu berhati-hati, jika terlalu banyak field yang tidak lengkap, akan terdapat rekod yang tidak seragam pada masa akan datang."></asp:Label></div>
<asp:Label ID="lblCourseID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>|
<asp:Label ID="lblClassID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>|

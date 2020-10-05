<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentuni_update.ascx.vb" Inherits="permatapintar.studentuni_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Maklumat Pengajian Tinggi
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Nama Pengajian Tinggi
        </td>
        <td>:<asp:TextBox ID="txtUniName" runat="server" Width="300px" MaxLength="250" Style="text-transform: uppercase;"></asp:TextBox>
        </td>
        <td class="fbtd_left">Tajaan
        </td>
        <td>:<asp:TextBox ID="txtUniTajaan" runat="server" Width="300px" MaxLength="250" Style="text-transform: uppercase;"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Bidang Pengajian
        </td>
        <td>:<asp:TextBox ID="txtUniCourse" runat="server" Width="300px" MaxLength="250" Style="text-transform: uppercase;"></asp:TextBox>
        </td>
        <td>Tahap Pengajian
        </td>
        <td>:<asp:DropDownList ID="ddlUniLevel" runat="server" AutoPostBack="false" Width="300px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Tahun Mula
        </td>
        <td>:<asp:DropDownList ID="ddlUniStartYear" runat="server" AutoPostBack="false" Width="300px">
        </asp:DropDownList>
        </td>
        <td>Tahun Tamat
        </td>
        <td>:<asp:DropDownList ID="ddlUniEndYear" runat="server" AutoPostBack="false" Width="300px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Negara
        </td>
        <td>:<asp:DropDownList ID="ddlUniCountry" runat="server" AutoPostBack="false" Width="300px">
        </asp:DropDownList>
        <td>Pengajian Terkini?</td>
        <td>
            <asp:CheckBox ID="chkIsLatest" runat="server" Text="YA" />
            [Hanya satu Pengajian terkini dibenarkan. Yang lain akan direset kepada 'N']
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;" colspan="3">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>
</table>

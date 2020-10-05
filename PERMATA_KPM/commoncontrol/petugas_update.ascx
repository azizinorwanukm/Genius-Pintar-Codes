<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="petugas_update.ascx.vb"
    Inherits="permatapintar.petugas_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini Petugas Pusat Ujian
        </td>
    </tr>
    <tr>
        <td>
            Jenis Petugas
        </td>
        <td>
            <select name="selUserType" id="selUserType" style="width: 200px;" runat="server">
                <option value="PEMANTAU JPN">PEMANTAU JPN</option>
                <option value="PEMANTAU PPD">PEMANTAU PPD</option>
                <option value="PEMANTAU UKM">PEMANTAU UKM</option>
                <option value="PENGAWAS">PENGAWAS</option>
                <option value="JURUTEKNIK">JURUTEKNIK</option>
            </select>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *Nama Penuh Petugas:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;[Seperti
            di dalam MYKAD]
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *Tel#:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *Email:
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Tujuan Pembayaran
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Bank:
        </td>
        <td>
            <asp:TextBox ID="txtBankName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Akaun#:
        </td>
        <td>
            <asp:TextBox ID="txtAccountNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini Maklumat Petugas" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>

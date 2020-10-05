<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_petugas_update.ascx.vb"
    Inherits="permatapintar.pusatujian_petugas_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Petugas
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Jenis Petugas
        </td>
        <td>
            <select name="selUserType" id="selUserType" style="width: 200px;" runat="server">
                <option value="PEMANTAU JPN">PEMANTAU JPN</option>
                <option value="PEMANTAU KPM">PEMANTAU KPM</option>
                <option value="PEMANTAU PPD">PEMANTAU PPD</option>
                <option value="PEMANTAU UKM">PEMANTAU UKM</option>
                <option value="PENGAWAS">PENGAWAS</option>
                <option value="JURUTEKNIK">JURUTEKNIK</option>
            </select>&nbsp;
        </td>
    </tr>
    <tr>
        <td>*MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>*Nama Petugas:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;[Seperti
            di dalam MYKAD]
        </td>
    </tr>
    <tr>
        <td>*Tel#:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>*Email:
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat Tempat Bekerja :
        </td>
        <td>
            <asp:TextBox ID="txtAddress1" runat="server" Width="350px" MaxLength="250"></asp:TextBox><br />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">&nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtAddress2" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtPostCode" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Tujuan Pembayaran
        </td>
    </tr>
    <tr>
        <td>Nama Bank:
        </td>
        <td>
            <asp:TextBox ID="txtBankName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Akaun#:
        </td>
        <td>
            <asp:TextBox ID="txtAccountNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
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
        <td>&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapus" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkSenaraiPetugas" runat="server">Senarai Petugas</asp:LinkButton>
        </td>
    </tr>
</table>


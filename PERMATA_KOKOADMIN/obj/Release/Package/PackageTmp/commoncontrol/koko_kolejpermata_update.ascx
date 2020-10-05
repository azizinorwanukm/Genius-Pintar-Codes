<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_kolejpermata_update.ascx.vb" Inherits="permatapintar.koko_kolejpermata_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Kokurikulum
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kokurikulum (BM):
        </td>
        <td>
            <asp:TextBox ID="txtNamaBM" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Kokurikulum (BI):
        </td>
        <td>
            <asp:TextBox ID="txtNamaBI" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Kod Kokurikulum:
        </td>
        <td>
            <asp:TextBox ID="txtKod" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Hari:
        </td>
        <td>
            <asp:TextBox ID="txtHari" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Masa:
        </td>
        <td>
            <asp:TextBox ID="txtMasa" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Tempat:
        </td>
        <td>
            <asp:TextBox ID="txtTempat" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
     <tr>
        <td>Mandatory:
        </td>
        <td>
            <select name="selIsMandatory" id="selIsMandatory" style="width: 300px;" runat="server">
                <option value="Y">Y</option>
                <option value="N" selected="selected">N</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kokurikulum</asp:LinkButton>
        </td>
    </tr>

</table>
<asp:Label ID="lblNamaOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>


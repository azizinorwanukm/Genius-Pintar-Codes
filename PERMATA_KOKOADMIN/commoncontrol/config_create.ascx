<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_create.ascx.vb" Inherits="permatapintar.config_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Konfigurasi
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
        <td >Kod Konfigurasi:
        </td>
        <td>
            <asp:TextBox ID="txtConfigCODE" runat="server" Width="300px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td >Tetapan:
        </td>
        <td>
            <asp:TextBox ID="txtConfigString" runat="server" Width="300px" MaxLength="250"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td >Catatan:
        </td>
        <td>
            <asp:TextBox ID="txtConfigDesc" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
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
            <asp:Button ID="btnCreate" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Konfigurasi</asp:LinkButton>
        </td>
    </tr>
    
</table>
<asp:Label ID="lblKelasOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kemudahan_create.ascx.vb" Inherits="permatapintar.kemudahan_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Kemudahan Baru
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
        <td>Nama Kemudahan:
        </td>
        <td>
            <asp:TextBox ID="txtKemudahan" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
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
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kemudahan</asp:LinkButton>
        </td>
    </tr>
   
</table>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_select.ascx.vb" Inherits="permatapintar.koko_select" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pilih Kokurikulum
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
        <td>Badan Beruniform:
        </td>
        <td>
            <asp:DropDownList ID="ddlUniform" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kelab & Persatuan:
        </td>
        <td>
            <asp:DropDownList ID="ddlPersatuan" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Sukan & Permainan:
        </td>
        <td>
             <asp:DropDownList ID="ddlSukan" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>


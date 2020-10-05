<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_update.ascx.vb" Inherits="permatapintar.koko_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pilih Kokurikulum
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kelas:
        </td>
        <td>
            <asp:DropDownList ID="ddlKelas" AutoPostBack="true" runat="server" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Badan Beruniform:
        </td>
        <td>
            <asp:DropDownList ID="ddlUniform" runat="server" AutoPostBack="true" Width="300px">
            </asp:DropDownList>
            &nbsp;Jumlah Pelajar:<asp:Label ID="lblCountUniform" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblMaxUniform" runat="server" Text="0"></asp:Label>&nbsp;<asp:Label ID="lblUniformlock" runat="server" Text="N"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Kelab & Persatuan:
        </td>
        <td>
            <asp:DropDownList ID="ddlPersatuan" runat="server" AutoPostBack="true" Width="300px">
            </asp:DropDownList>
            &nbsp;Jumlah Pelajar:<asp:Label ID="lblCountPersatuan" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblMaxPersatuan" runat="server" Text="0"></asp:Label>&nbsp;<asp:Label ID="lblPersatuanlock" runat="server" Text="N"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Sukan & Permainan:
        </td>
        <td>
            <asp:DropDownList ID="ddlSukan" runat="server" AutoPostBack="true" Width="300px">
            </asp:DropDownList>
            &nbsp;Jumlah Pelajar:<asp:Label ID="lblCountSukan" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblMaxSukan" runat="server" Text="0"></asp:Label>&nbsp;<asp:Label ID="lblSukanlock" runat="server" Text="N"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Rumah Sukan:
        </td>
        <td>
            <asp:DropDownList ID="ddlRumahsukan" runat="server" AutoPostBack="true" Width="300px">
            </asp:DropDownList>
            &nbsp;Jumlah Pelajar:<asp:Label ID="lblCountRumahsukan" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblMaxRumahsukan" runat="server" Text="0"></asp:Label>&nbsp;<asp:Label ID="lblRumahsukanlock" runat="server" Text="N"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Tarikh Akhir Kemaskini:</td>
        <td>
            <asp:Label ID="lblKOKOEND" runat="server" Text="0"></asp:Label>&nbsp;[YYYYMMDD]</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server" Visible="false">Senarai Pelajar</asp:LinkButton>
        </td>
    </tr>
</table>


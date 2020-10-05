<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengumuman_update.ascx.vb" Inherits="permatapintar.pengumuman_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Pengumuman Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Paparkan:
        </td>
        <td>
           <select name="selIsDisplay" id="selIsDisplay" style="width: 100px;" runat="server">
                <option value="Y" selected="selected">YA</option>
                <option value="N">TIDAK</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>Tajuk:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="600px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Pengumuman:
        </td>
        <td>
            <asp:TextBox ID="txtBody" runat="server" Width="600px" TextMode="MultiLine" Rows="10"></asp:TextBox>&nbsp;
        </td>
    </tr>
     <tr>
        <td>Tarikh:
        </td>
        <td>
            <asp:Label ID="lblDateCreated" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>

    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Pengumuman</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_check.ascx.vb" Inherits="permatapintar.user_check" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Log Masuk Pelajar
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            <asp:Label ID="Label1" runat="server" Text="MYKAD/MYKID# Pelajar"></asp:Label>
        </td>
        <td>:<asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px" MaxLength="20"></asp:TextBox>&nbsp;*&nbsp;[Contoh MYKAD/MYKID#:123456121234. Tanpa "-"]
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Kata Laluan"></asp:Label>
        </td>
        <td>:<select name="selMYKAD" id="selMYKAD" style="width: 200px;" runat="server">
            <option value="0" selected="selected">--Pilih Jenis MYKAD--</option>
            <option value="1">MYKAD Ibu</option>
            <option value="2">MYKAD Bapa</option>
        </select>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            <asp:Label ID="lblMYKADSearch" runat="server" Text="MYKAD#"></asp:Label>
        </td>
        <td>:<asp:TextBox ID="txtMYKADSearch" runat="server" Text="" Width="200px" MaxLength="20" TextMode="Password"></asp:TextBox>&nbsp;*&nbsp;[Contoh MYKAD# Ibu/Bapa:123456121234. Tanpa "-"]
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="- Medan bertanda * adalah wajib diisi." CssClass="fblabel_msg"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <asp:Button ID="btnNext" runat="server" Text="Hantar" CssClass="fbbutton" />&nbsp;<asp:Button ID="btnReset" runat="server" Text="Set Semula" CssClass="fbbutton" />&nbsp;
            
        </td>
    </tr>
</table>
<div class="warning" id="divMsg" runat="server">
    <p>
        - Sila kemaskini MYKAD/MYKID# anda di laman ini. Gunakan MYKAD/MYKID# 12 digit untuk menduduki Ujian UKM1.<br />
        - Sebarang aksara bukan nombor di dalam MYKAD/MYKID# tidak dibenarkan dilaman Ujian UKM1.
    </p>
</div>

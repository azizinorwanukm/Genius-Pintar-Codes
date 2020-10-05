<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_sesssion_add.ascx.vb" Inherits="permatapintar.config_sesssion_add" %>

<table class="fbform">
    <tr>
        <td>Nama Session : </td>
        <td> <asp:TextBox ID="txt_namaSession" runat="server" ></asp:TextBox></td>
    </tr>
    <tr>
        <td >Tahun : </td>
        <td> <asp:TextBox ID="txtYear" runat ="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Button runat="server" Text="Tambah Session" id="btn_tambahsession"/>
        </td>
    </tr>
</table>
<asp:RequiredFieldValidator runat="server" ControlToValidate="txt_namaSession" ErrorMessage="Error: Field must not be blank"></asp:RequiredFieldValidator><br />      
<asp:RequiredFieldValidator runat="server" ControlToValidate="txtYear" ErrorMessage="Error: Enter a valid year"></asp:RequiredFieldValidator><br />
<asp:RangeValidator Type="Integer" MinimumValue="1990" MaximumValue="2200" ControlToValidate="txtYear" runat="server" ErrorMessage="Error: Enter a valid year"></asp:RangeValidator>
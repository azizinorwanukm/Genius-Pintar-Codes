<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_create.ascx.vb" Inherits="araken.pcisadmin.userprofile_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Pelajar
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">MYKAD/MYKID#
        </td>
        <td>:<asp:TextBox ID="lblicno" runat="server" Width="250px" MaxLength="25" Text=""></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Nama Penuh
        </td>
        <td>:<asp:TextBox ID="lblfullname" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat Rumah
        </td>
        <td>:<asp:TextBox ID="lbladdress" runat="server" Width="500px" MaxLength="250" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:TextBox ID="lblphoneno" runat="server" Width="250px" MaxLength="50" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Email
        </td>
        <td>:<asp:TextBox ID="lblemail" runat="server" Width="250px" MaxLength="100" Text=""></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td>Nama Ibu
        </td>
        <td>:<asp:TextBox ID="lblmothername" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Pekerjaan Ibu
        </td>
        <td>:<asp:TextBox ID="lblmotheroccupation" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Nama Bapa
        </td>
        <td>:<asp:TextBox ID="lblfathername" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Pekerjaan Bapa
        </td>
        <td>:<asp:TextBox ID="lblfatheroccupation" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Taska / Tadika
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pusat
        </td>
        <td>:<asp:TextBox ID="lbllearningcentrename" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Alamat
        </td>
        <td>:<asp:TextBox ID="lbllearningcentreaddress" runat="server" Width="500px" MaxLength="250" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri
        </td>
        <td>:<asp:DropDownList ID="ddllearningcentrestatename" runat="server" Width="500px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:TextBox ID="lbllearningcentrephoneno" runat="server" Width="250px" MaxLength="50" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Nama Pembantu
        </td>
        <td>:<asp:TextBox ID="lblassistantname" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td>Tel#
        </td>
        <td>:<asp:TextBox ID="lblassistantphoneno" runat="server" Width="250px" MaxLength="50" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2"></td>
    </tr>
    <tr>
        <td style="text-align: left;" colspan="2">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="fbbutton" />
        </td>
    </tr>
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>

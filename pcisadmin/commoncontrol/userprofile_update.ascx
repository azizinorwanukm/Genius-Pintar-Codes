<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_update.ascx.vb" Inherits="araken.pcisadmin.userprofile_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Maklumat Pelajar
        </td>
        <td style="text-align: right;">&nbsp;
        </td>
    </tr>
    <tr>
        <td>MYKAD/MYKID#
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
        <td class="fbtd_left">Maklumat PAPN
        </td>
        <td style="text-align: right;">&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Pusat
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
        <td>&nbsp;</td>
        <td></td>
    </tr>
</table>
<table class="fbform">
    <tr>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkView" runat="server">Profil Pelajar</asp:LinkButton>

        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblicno_bak" runat="server" Text=""></asp:Label>

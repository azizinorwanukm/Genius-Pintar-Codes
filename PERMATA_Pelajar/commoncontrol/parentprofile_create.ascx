<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parentprofile_create.ascx.vb"
    Inherits="permatapintar.parentprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Bapa/Penjaga
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherFullname" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Bapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="txtFatherJob" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            Tahap Pendidikan Bapa/Penjaga:
        </td>
        <td>
            <select name="selFatherEducation" id="selFatherEducation" style="width: 255px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="Phd dan ke atas">Phd dan ke atas</option>
                <option value="Master">Master</option>
                <option value="Ijazah">Ijazah</option>
                <option value="Diploma">Diploma</option>
                <option value="Sijil">Sijil</option>
                <option value="STPM">STPM</option>
                <option value="SPM dan ke-bawah">SPM dan ke-bawah</option>
            </select>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Ibu
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherFullname" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtMotherJob" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            Tahap Pendidikan Ibu:
        </td>
        <td>
            <select name="selMotherEducation" id="selMotherEducation" style="width: 255px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="Phd dan ke atas">Phd dan ke atas</option>
                <option value="Master">Master</option>
                <option value="Ijazah">Ijazah</option>
                <option value="Diploma">Diploma</option>
                <option value="Sijil">Sijil</option>
                <option value="STPM">STPM</option>
                <option value="SPM dan ke-bawah">SPM dan ke-bawah</option>
            </select>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Keluarga
        </td>
    </tr>
    <tr>
        <td>
            Pendapatan Sekeluarga:
        </td>
        <td>
            <select name="selFamilyIncome" id="selFamilyIncome" style="width: 255px;" runat="server">
                <option value="kurang RM1500">kurang RM1500</option>
                <option value="antara RM1501 hingga RM3500">antara RM1501 hingga RM3500</option>
                <option value="antara RM3501 hingga RM5500">antara RM3501 hingga RM5500</option>
                <option value="antara RM5501  hingga RM10000">antara RM5501  hingga RM10000</option>
                <option value="lebih RM10000">lebih RM10000</option>
            </select>&nbsp;*&nbsp;sebulan
        </td>
    </tr>
    <tr>
        <td>
            Nombor Talipon:
        </td>
        <td>
            <asp:TextBox ID="txtFamilyContactNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align:right;">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Maklumat Ibubapa/Penjaga" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
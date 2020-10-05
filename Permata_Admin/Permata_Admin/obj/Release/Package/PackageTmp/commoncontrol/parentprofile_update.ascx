<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parentprofile_update.ascx.vb" Inherits="permatapintar.parentprofile_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Bapa/ Penjaga
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtFatherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFatherFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>Pekerjaan:
        </td>
        <td>
            <asp:TextBox ID="txtFatherJob" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>Tahap Pendidikan:
        </td>
        <td>
            <select name="selFatherEducation" id="selFatherEducation" style="width: 255px;" runat="server">
                <option value="" selected="selected">--Pilih--</option>
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
        <td colspan="2">Maklumat Ibu
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMotherMYKADNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtMotherFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>Pekerjaan:
        </td>
        <td>
            <asp:TextBox ID="txtMotherJob" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>Tahap Pendidikan:
        </td>
        <td>
            <select name="selMotherEducation" id="selMotherEducation" style="width: 255px;" runat="server">
                <option value="" selected="selected">--Pilih--</option>
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
        <td colspan="2">Maklumat Keluarga
        </td>
    </tr>
    <tr>
        <td>Pendapatan Sekeluarga:
        </td>
        <td>
            <select name="selFamilyIncome" id="selFamilyIncome" style="width: 255px;" runat="server">
                <option value="">--Pilih--</option>
                <option value="kurang RM1500">kurang RM1500</option>
                <option value="antara RM1501 hingga RM3500">antara RM1501 hingga RM3500</option>
                <option value="antara RM3501 hingga RM5500">antara RM3501 hingga RM5500</option>
                <option value="antara RM5501  hingga RM10000">antara RM5501  hingga RM10000</option>
                <option value="lebih RM10000">lebih RM10000</option>
            </select>&nbsp;sebulan
        </td>
    </tr>
    <tr>
        <td>Jumlah Ahli Keluarga:
        </td>
        <td>
            <select name="selJumlahAnak" id="selJumlahAnak" style="width: 255px;" runat="server">
                <option value="0">--Pilih--</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
                <option value="6">6</option>
                <option value="7">7</option>
                <option value="8">8</option>
                <option value="9">9</option>
                <option value="10">10</option>
                <option value="11">11</option>
                <option value="12">12</option>
                <option value="13">13</option>
                <option value="14">14</option>
                <option value="15">15</option>
            </select>&nbsp;[Yang masih bersekolah]
        </td>
    </tr>
    <tr>
        <td>Nombor Talipon Bapa:
        </td>
        <td>
            <asp:TextBox ID="txtFamilyContactNo" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*&nbsp;[Masukkan NA jika tiada]
        </td>
    </tr>
    <tr>
        <td>Nombor Talipon Ibu:
        </td>
        <td>
            <asp:TextBox ID="txtFamilyContactNoIbu" runat="server" Width="250px" MaxLength="50"></asp:TextBox>&nbsp;*&nbsp;[Masukkan NA jika tiada]
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>
</table>
System ID:<asp:Label ID="lblParentID" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
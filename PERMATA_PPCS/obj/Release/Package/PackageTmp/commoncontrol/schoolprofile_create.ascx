<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_create.ascx.vb"
    Inherits="permatapintar.schoolprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Sekolah Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            Kod Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCode" runat="server" Width="80px" MaxLength="10" Text=""></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolName" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Alamat Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolAddress" runat="server" Width="350px" MaxLength="500"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolPostcode" runat="server" Width="80px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCity" runat="server" Width="250px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            PPD
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolPPD" runat="server" Width="250px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>
            Jenis Sekolah
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolType" runat="server" Width="250px">
            </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>
            Tel.#
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoTel" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Fax#
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolEmail" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Lokasi
        </td>
        <td>
            :<select name="selSchoolLokasi" id="selSchoolLokasi" style="width: 200px;" runat="server">
                <option value="B">BANDAR</option>
                <option value="LB">LUAR BANDAR</option>
            </select>*
        </td>
    </tr>
    <tr>
        <td>
            SchoolID
        </td>
        <td>
            :<asp:Label ID="lblSchoolID" runat="server" Text=""></asp:Label>
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
        <td style="text-align: left;">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Sekolah Baru" CssClass="fbbutton" />&nbsp;
            <asp:LinkButton ID="lnkStudentProfileView" runat="server">View Student Profile</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>

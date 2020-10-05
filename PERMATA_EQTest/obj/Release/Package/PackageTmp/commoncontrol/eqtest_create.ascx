<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="eqtest_create.ascx.vb" Inherits="PERMATA_EQTest.eqtest_create" %>

<p>
    Sila daftar terlebih dahulu sebelum mengambil ujian ini. &nbsp;<asp:Label ID="lblCompanyname" runat="server" Text="" Font-Bold="true"></asp:Label>.
</p>
<p>Ujian ini memerlukan <b>Microsoft Internet Explorer</b> dan <b>JAVA Virtual Machine</b>. Klik <a href="jvm.help.aspx" target="_blank">di sini untuk manual java dan semakan.</a>. Chrome telah menamatkan support ke atas Java Applet.</p>
<table id="mycustomtable">
    <tr>
        <th colspan="2">Daftar 
        </th>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text="Nama Penuh"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Text="" Width="300px"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblAge" runat="server" Text="Umur"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAge" runat="server" Text="" Width="300px"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblEmailAdd" runat="server" Text="EMail"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmailAdd" runat="server" Text="" Width="300px"></asp:TextBox>* (Keputusan akan dihantar ke EMail ini)
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPosition" runat="server" Text="Pekerjaan"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPosition" runat="server" Text="" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Teruskan" CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsgOK" runat="server" Text="Jika anda sudah berdaftar, sila lihat email anda untuk pautan ke ujian ini." ForeColor="Blue"></asp:Label>
            <asp:Label ID="lblSurveyID" runat="server" Text="" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="lblMsgNOK" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>


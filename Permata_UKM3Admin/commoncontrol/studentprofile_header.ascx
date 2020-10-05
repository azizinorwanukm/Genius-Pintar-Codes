<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_header.ascx.vb"
    Inherits="permatapintar.studentprofile_header" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="5">Maklumat Pelajar:
            <asp:Label ID="lblPPCSDate" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">ID#
        </td>
        <td>:&nbsp;<asp:Label ID="lblMYKAD" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td>Kod Kursus
        </td>
        <td>:&nbsp;<asp:Label ID="lblPPCSCourse" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td rowspan="6" style="vertical-align: top;">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgStudent" Style="width: 120px; height: 150px;" runat="server" />
                    </td>
                </tr>
               
            </table>
        </td>
    </tr>
    <tr>
        <td>Nama Penuh
        </td>
        <td>:&nbsp;<asp:Label ID="lblRespFullname" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td>Kod Kelas
        </td>
        <td>:&nbsp;<asp:Label ID="lblPPCSClass" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Sekolah
        </td>
        <td>:&nbsp;<asp:Label ID="lblSchoolName" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td>Ketua Modul
        </td>
        <td>:&nbsp;<asp:Label ID="lblNamaKetuaModul" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:&nbsp;<asp:Label ID="lblSchoolState" runat="server" Text="" ForeColor="Black"></asp:Label>&nbsp;&nbsp;
        </td>
        <td>Pembantu Pelajar
        </td>
        <td>:&nbsp;<asp:Label ID="lblNamaPembantuPelajar" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Instruktor
        </td>
        <td>:&nbsp;<asp:Label ID="lblNamaPengajar" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td>Pembantu Pengajar
        </td>
        <td>:&nbsp;<asp:Label ID="lblNamaPembantuPengajar" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td colspan="3">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>

</table>
<asp:Label ID="lblStudentID" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>
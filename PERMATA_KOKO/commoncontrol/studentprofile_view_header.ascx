<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_view_header.ascx.vb" Inherits="permatapintar.studentprofile_view_header" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Maklumat Pelajar:&nbsp;<asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%; vertical-align: top;">Gambar
        </td>
        <td colspan="3">
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
        <td class="fbtd_left">MYKAD/MYKID#
        </td>
        <td>:<asp:Label ID="lblMYKAD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>#Pelajar
        </td>
        <td>:<asp:Label ID="lblNoPelajar" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Jantina
        </td>
        <td>:<asp:Label ID="lblStudentGender" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Tarikh Lahir
        </td>
        <td>:<asp:Label ID="lblStudentDOB" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Bangsa
        </td>
        <td>:<asp:Label ID="lblStudentRace" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            &nbsp;&nbsp;</td>
        <td>Agama</td>
        <td>:<asp:Label ID="lblStudentReligion" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
</table>

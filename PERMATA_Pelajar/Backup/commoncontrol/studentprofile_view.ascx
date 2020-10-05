<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_view.ascx.vb"
    Inherits="permatapintar.studentprofile_view1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Pelajar
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Black">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            Gambar
        </td>
        <td>
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
        <td style="width: 20%;">
            MYKAD/MYKID#
        </td>
        <td>
            :<asp:Label ID="lblMYKAD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama Penuh
        </td>
        <td>
            :<asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Jantina
        </td>
        <td>
            :<asp:Label ID="lblStudentGender" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Tarikh Lahir
        </td>
        <td>
            :<asp:Label ID="lblStudentDOB" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Darjah/Tingkatan
        </td>
        <td>
            :<asp:Label ID="lblStudentForm" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Bangsa
        </td>
        <td>
            :<asp:Label ID="lblStudentRace" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            &nbsp;&nbsp;Agama:&nbsp;
            <asp:Label ID="lblStudentReligion" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            Kebolehan Berbahasa
        </td>
        <td>
            <table class="fbform">
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Pertuturan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkBM" runat="server" Text="B. Malaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkBI" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkMan" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkTamil" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkArab" runat="server" Text="B. Arab" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Penulisan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteBM" runat="server" Text="B. Malaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteBI" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteMan" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteTamil" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteArab" runat="server" Text="B. Arab" />&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Alamat Rumah
        </td>
        <td>
            :<asp:Label ID="lblStudentAddress1" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            &nbsp;
        </td>
        <td>
            :<asp:Label ID="lblStudentAddress2" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:Label ID="lblStudentPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:Label ID="lblStudentCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            &nbsp; &nbsp; Negeri:&nbsp;
            <asp:Label ID="lblStudentState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:Label ID="lblStudentEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>

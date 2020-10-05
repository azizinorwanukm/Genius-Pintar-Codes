<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_view.ascx.vb"
    Inherits="permatapintar.studentprofile_view1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>Maklumat Pelajar
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Gambar
        </td>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgStudent" Style="width: 120px; height: 150px;" runat="server" OnLoad="Page_Load" />
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
    </tr>
    <tr>
        <td>Nama Pelajar
        </td>
        <td>:<asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Jantina
        </td>
        <td>:<asp:Label ID="lblStudentGender" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir
        </td>
        <td>:<asp:Label ID="lblStudentDOB" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Darjah/Tingkatan
        </td>
        <td>:<asp:Label ID="lblStudentForm" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Bangsa:
        </td>
        <td>:<asp:Label ID="lblStudentRace" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Agama</td>
        <td>:<asp:Label ID="lblStudentReligion" runat="server" Text="" CssClass="fblabel_view"></asp:Label></td>
    </tr>
    <tr>
        <td>Alamat Rumah
        </td>
        <td>:<asp:Label ID="lblStudentAddress1" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Poskod
        </td>
        <td>:<asp:Label ID="lblStudentPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>&nbsp;    
        </td>
    </tr>
    <tr>
        <td>Bandar:</td>
        <td>:<asp:Label ID="lblStudentCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label></td>
    </tr>
   
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:Label ID="lblStudentState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Negara
        </td>
        <td>:<asp:Label ID="lblStudentCountry" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Email
        </td>
        <td>:<asp:Label ID="lblStudentEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tel Number
        </td>
        <td>:<asp:Label ID="lblStudentContactNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
     <tr>
        <td>Data dari Instuktor Ra PPCS sudah dimasukkan
        </td>
        <td>:<asp:Label ID="lbl_insRa" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
        <tr>
        <td>Data dari Instruktor KPP sudah dimasukkan
        </td>
        <td>:<asp:Label ID="lbl_insKpp" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
      <tr>
        <td>Data dari Instruktor PPCS sudah dimasukkan
        </td>
        <td>:<asp:Label ID="lbl_insPpcs" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>

</table>

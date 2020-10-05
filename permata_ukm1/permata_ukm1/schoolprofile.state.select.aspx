<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="schoolprofile.state.select.aspx.vb" Inherits="permatapintar.schoolprofile_state_select" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    <br />
    <b>*Menggunakan 1 atau 2 perkataan sahaja untuk kata kunci sekolah. Contoh Carian Sekolah:</b>
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                NAMA SEKOLAH
            </td>
            <td>
                NEGERI
            </td>
            <td>
                KATA KUNCI SEKOLAH
            </td>
        </tr>
        <tr>
            <td>
                SJK(C) BIN CHONG
            </td>
            <td>
                JOHOR
            </td>
            <td style="color: Red; font-weight: bold;">
                BIN CHONG
            </td>
        </tr>
        <tr>
            <td>
                SMJK PEREMPUAN CHINA PULAU PINANG
            </td>
            <td>
                PULAU PINANG
            </td>
            <td style="color: Red; font-weight: bold;">
                PEREMPUAN CHINA
            </td>
        </tr>
        <tr>
            <td>
                SMK PUTRAJAYA PRESINT 18(1)
            </td>
            <td>
                WP PUTRAJAYA
            </td>
            <td style="color: Red; font-weight: bold;">
                PRESINT
            </td>
        </tr>
        <tr>
            <td>
                SK ORANG KAYA MOHAMAD
            </td>
            <td>
                SARAWAK
            </td>
            <td style="color: Red; font-weight: bold;">
                KAYA
            </td>
        </tr>
        <tr>
            <td>
                SEKOLAH SUKAN TUNKU MAHKOTA ISMAIL
            </td>
            <td>
                JOHOR
            </td>
            <td style="color: Red; font-weight: bold;">
                MAHKOTA
            </td>
        </tr>
    </table>
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Pilih negeri dimana sekolah anda berada.
            </td>
        </tr>
        <tr>
            <td>
                Negeri:&nbsp;
                <asp:DropDownList ID="ddlSchoolState" Width="200" AutoPostBack="false" runat="server"
                    Height="25">
                </asp:DropDownList>
                *&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkBack" runat="server">Kembali</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
</asp:Content>

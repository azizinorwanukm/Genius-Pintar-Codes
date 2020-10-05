<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.schoolprofile.search.change.aspx.vb" Inherits="permatapintar.subadmin_schoolprofile_search_change" %>

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
            <td>NAMA SEKOLAH
            </td>
            <td>NEGERI
            </td>
            <td>KATA KUNCI SEKOLAH
            </td>
        </tr>
        <tr>
            <td>SJK(C) BIN CHONG
            </td>
            <td>JOHOR
            </td>
            <td>BIN CHONG
            </td>
        </tr>
        <tr>
            <td>SMJK PEREMPUAN CHINA PULAU PINANG
            </td>
            <td>PULAU PINANG
            </td>
            <td>PEREMPUAN CHINA
            </td>
        </tr>
        <tr>
            <td>SMK PUTRAJAYA PRESINT 18(1)
            </td>
            <td>WP PUTRAJAYA
            </td>
            <td>PRESINT
            </td>
        </tr>
        <tr>
            <td>SK ORANG KAYA MOHAMAD
            </td>
            <td>SARAWAK
            </td>
            <td>KAYA
            </td>
        </tr>
        <tr>
            <td>SEKOLAH SUKAN TUNKU MAHKOTA ISMAIL
            </td>
            <td>JOHOR
            </td>
            <td>MAHKOTA
            </td>
        </tr>
    </table>
    &nbsp;
    <div class="info">
        • Sekolah Kementerian Pelajaran Malaysia dan MRSM mempunyai kod - contoh ABA0001
        atau BEA8612<br />
    </div>
    <table class="fbform">
        <tr class="fbform_header">
            <td>Sekolah Rendah
            </td>
            <td>Sekolah Menengah
            </td>
        </tr>
        <tr>
            <td>SK<br />
                SK (Khas)<br />
                SK (Asli)<br />
                SJK (C)<br />
                SJK (T)<br />
                SR Agama (SABK)<br />
                SRK<br />
                SR Model Khas Komprehensif K9
            </td>
            <td>Sekolah Seni<br />
                Sekolah Sukan
                <br />
                SM Agama (SABK)<br />
                SM + SR (Model Khas)<br />
                SM Berasrama Penuh<br />
                SM Khas<br />
                SMK<br />
                SMK Agama<br />
                MRSM
            </td>
        </tr>
    </table>
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Carian Sekolah
            </td>
        </tr>
        <tr>
            <td>Negeri:&nbsp;
                <asp:DropDownList ID="ddlSchoolState" AutoPostBack="true" runat="server">
                </asp:DropDownList>
                * Bandar:&nbsp;
                <asp:DropDownList ID="ddlSchoolCity" runat="server" Width="200px">
                </asp:DropDownList>
                Nama Sekolah:&nbsp;
                <asp:TextBox ID="txtSchoolName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;
                
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;</td>
        </tr>
    </table>

    <table class="fbform">
        <tr class="fbform_header">
            <td>Keputusan Carian Sekolah
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="SchoolID"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bandar">
                            <ItemTemplate>
                                <asp:Label ID="lblBandar" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri">
                            <ItemTemplate>
                                <asp:Label ID="txtNegeri" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Sekolah Baru " CssClass="fbbutton"
                    Visible="false" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNewSchool" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="schoolprofile.search.select.aspx.vb" Inherits="permatapintar.schoolprofile_search_select" %>

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
                KATA KUNCI NAMA SEKOLAH
            </td>
        </tr>
        <tr>
            <td>
                SJK(C) BIN CHONG
            </td>
            <td style="color: Red; font-weight: bold;">
                BIN CHONG
            </td>
        </tr>
        <tr>
            <td>
                SMJK PEREMPUAN CHINA PULAU PINANG
            </td>
            <td style="color: Red; font-weight: bold;">
                PEREMPUAN CHINA
            </td>
        </tr>
        <tr>
            <td>
                SMK PUTRAJAYA PRESINT 18(1)
            </td>
            <td style="color: Red; font-weight: bold;">
                PRESINT
            </td>
        </tr>
        <tr>
            <td>
                SK ORANG KAYA MOHAMAD
            </td>
            <td style="color: Red; font-weight: bold;">
                KAYA
            </td>
        </tr>
        <tr>
            <td>
                SEKOLAH SUKAN TUNKU MAHKOTA ISMAIL
            </td>
            <td style="color: Red; font-weight: bold;">
                MAHKOTA
            </td>
        </tr>
    </table>
    <div class="info">
        • Kod Sekolah di bawah Kementerian Pelajaran Malaysia atau MRSM seperti ABA0001,
        YEE1204 atau BEA8612<br />
        • BUKAN POSKOD!
    </div>
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Carian Sekolah. Negeri:
                <asp:Label ID="lblSchoolState" runat="server" Text="" Font-Bold="true" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Menggunakan Kod Sekolah
            </td>
            <td>
                :<asp:TextBox ID="txtSchoolCode" runat="server" Width="100px" MaxLength="50"></asp:TextBox>&nbsp;(Tanpa
                ruang kosong atau space) ATAU
            </td>
        </tr>
        <tr>
            <td>
                Menggunakan Kata Kunci Nama Sekolah
            </td>
            <td>
                :<asp:TextBox ID="txtSchoolName" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnChange" runat="server" Text="Tukar Negeri " CssClass="fbbutton" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkBack" runat="server">Kembali</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Keputusan Carian Sekolah
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
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Sekolah" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bandar" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblBandar" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri" HeaderStyle-HorizontalAlign="Left"> 
                            <ItemTemplate>
                                <asp:Label ID="txtNegeri" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
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
                <asp:Label ID="lblNoRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                <asp:Label Font-Size="Large" ForeColor="Red" ID="lblNewSchool" runat="server" Text="Nota:<br />Jika Pelajar/Guru/Ibubapa  tidak memilih sekolah sedia ada dalam senarai database KPM dan MARA, maka pelajar tidak akan dipertimbangkan untuk ke ujian UKM2. Kod <b>XXX</b> hanya untuk <b>sekolah antarabangsa dan persendirian sahaja.</b>"></asp:Label>
            </td>
        </tr>
    </table>
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Sekolah Rendah
            </td>
            <td>
                Sekolah Menengah
            </td>
        </tr>
        <tr>
            <td>
                SK<br />
                SK (Khas)<br />
                SK (Asli)<br />
                SJK (C)<br />
                SJK (T)<br />
                SR Agama (SABK)<br />
                SRK<br />
                SR Model Khas Komprehensif K9
            </td>
            <td>
                Sekolah Seni<br />
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
</asp:Content>

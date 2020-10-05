<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="ukm1.school.search.aspx.vb" Inherits="permatapintar.ukm1_school_search"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0px" style="background-color: #eceff6;">
        <tr>
            <td colspan="2" class="fbsection_header">
                <a href="ukm1.update.student.aspx">Maklumat Pelajar</a> | <a href="ukm1.update.school.aspx">
                    :: Maklumat Sekolah ::</a> | <a href="ukm1.update.parent.aspx">Maklumat Ibubapa/Penjaga</a>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                Nama Sekolah:
            </td>
            <td>
                <asp:TextBox ID="txtNamaSekolah" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;<br />
                <asp:Label ID="Label4" runat="server" Text="[ Masukkan Nama Sekolah. Contoh KOLEJ TUNKU KURSHIAH, masukkan KURSHIAH sahaja dan klik Mula Carian. ]"
                    CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="fbform_sap">
                <asp:Label ID="lblFormMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Mula Carian" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                [ Masukkan nama sekolah anda dan tekan Mula Carian. Tekan PILIH untuk ke laman seterusnya.
                Maklumat lengkap sekolah anda akan dipaparkan.]
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table style="width: 100%; border: solid 1px #e2c822">
                    <tr>
                        <td>
                            • Semasa mengisi maklumat pelajar, sekolah dan ibu bapa / penjaga, jika didapati
                            maklumat palsu, walaupun pelajar melepasi skor kelayakan, maka pelajar tidak layak
                            menduduki ujian UKM2 atau USIM-1.<br />
                            • Sekiranya orang lain yang menjawab soalan bagi pihak pelajar atau pelajar meniru,
                            maka pelajar akan terperangkap semasa ujian saringan kedua UKM2 yang dilakukan dengan
                            pengawasan.<br />
                            • Sila <b>JUJUR dan BERTANGGUNGJAWAB</b> pada diri sendiri semasa mengisi maklumat
                            dan menjawab soalan.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td class="fbsection_header">
                Keputusan Carian Sekolah [Klik PILIH untuk sekolah anda.]
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
                            <EditItemTemplate>
                                <asp:TextBox ReadOnly="true" Width="20px" ID="lableIndex" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lableIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Sekolah" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bandar" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox Width="80px" ID="txtBandar" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBandar" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox Width="80px" ID="txtNegeri" runat="server" Text='<%# Bind("SchoolState") %>'></asp:TextBox>
                            </EditItemTemplate>
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
                Jumlah Rekod:
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                &nbsp;
                <asp:Label ID="lblPageNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />
                <asp:Label ID="Label3" runat="server" Text="[Adakah anda sudah membuat carian? Sekolah anda tiada dalam senarai? Tekan Seterusnya >>]"
                    CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

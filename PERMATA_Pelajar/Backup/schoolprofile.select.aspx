<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/popup.Master"
    CodeBehind="schoolprofile.select.aspx.vb" Inherits="permatapintar.schoolprofile_select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Carian Sekolah Anda
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                Nama Sekolah:
            </td>
            <td>
                <asp:TextBox ID="txtSchoolName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnLoad" runat="server" Text="Mula Carian" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info">
        • Carian hanya untuk sekolah yang berdaftar dengan Kem. Pelajaran Malaysia sahaja.<br />
        • Masukkan Nama Sekolah atau Kod Sekolah. Contoh masukkan 'Telok Intan' untuk 'SM
        (SAINS) TELOK INTAN'</div>
    <div class="warning">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Keputusan Carian Sekolah [Tekan PILIH untuk sekolah anda.]
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="SchoolCode"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Top" />
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
                        <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
                        <asp:TemplateField>
                            <AlternatingItemTemplate>
                                <asp:Button ID="btnSelect" runat="server" Text="Pilih" />
                            </AlternatingItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnSelect" runat="server" Text="Pilih" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <input type="hidden" id="control" runat="server" name="control">
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_kodsekolah_summary.ascx.vb" Inherits="permatapintar.ukm2_kodsekolah_summary" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Ringkasan Ujian mengikut Kod Sekolah.
        </td>
    </tr>
    <tr>
        <td style="width:150px">Kod Sekolah:</td>
        <td>
            <asp:TextBox ID="txtKodSekolah" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width:150px">Nama Pelajar:</td>
        <td>
            <asp:TextBox ID="txtFilterNamaPelajar" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:CheckBox ID="chkRuleAge" runat="server" Text="Pelajar berumur 8-15 tahun (8 tahun - beragama Islam sahaja)" Checked="false" />
        </td>
    </tr>
    <tr>
        <td style="width:150px"></td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>

    
    <tr>
        <td colspan="2">
            <table style="width: 70%;" border="0">
                <tr style="background-color: #5D7B9D; color: White">
                    <td style="margin-top: 10px; margin-bottom: 10px"><b># </b></td>
                    <td><b>Nama Sekolah </b></td>
                    <td><b>Negeri </b></td>
                    <td><b>#Pelajar </b></td>
                </tr>
                <tr style="background-color: #F7F6F3;">
                    <td style="margin-top: 10px; margin-bottom: 10px">
                        <asp:Label ID="LblNo" runat="server" Text=""></asp:Label></td>
                    <td>
                        <asp:Label ID="LblSchoolName" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblNegeri" runat="server" Text=""></asp:Label></td>
                    <td>
                        <asp:Label ID="Lbljumlah" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>


</table>

<br />

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td>Senarai Pelajar</td>       
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="StudentID"
                Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaPelajar" runat="server" Text='<%# Bind("StudentFullname")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="lblTahunLahir" runat="server" Text='<%# Bind("DOB_Year")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kod Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="ddlKodSekolah" runat="server" Text='<%# Bind("SchoolCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Terima Sijil">
                        <ItemTemplate>
                            <asp:Label ID="lblTerimaSijil" runat="server"  Text='<%# Bind("isLayakSijil") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
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
    <tr class="fbform_msg">
        <td class="auto-style2" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>

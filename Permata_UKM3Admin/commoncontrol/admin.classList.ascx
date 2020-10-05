<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.classList.ascx.vb" Inherits="permatapintar.admin_classList" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Senarai Kelas
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sesi PPCS:</td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
            &nbsp;
        </td>
        <td>Nama Kelas-BM:</td>
        <td>
            <asp:TextBox ID="txtClassNameBM" runat="server" Width="300px" MaxLength="50"></asp:TextBox>&nbsp;
            
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbaside_sap" colspan="3">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnRefresh" runat="server" Text="Cari " CssClass="fbbutton" /></td>
        <td></td>
        <td></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kelas
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ClassID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ketua Modul-BM">
                        <ItemTemplate>
                            <asp:Label ID="NamaKetuaModul" runat="server" Text='<%# Bind("NamaKetuaModul")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ketua Modul-BI">
                        <ItemTemplate>
                            <asp:Label ID="NamaKetuaModulBI" runat="server" Text='<%# Bind("NamaKetuaModulBI")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Kelas">
                        <ItemTemplate>
                            <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Kelas-BM">
                        <ItemTemplate>
                            <asp:Label ID="lblClassNameBM" runat="server" Text='<%# Bind("ClassNameBM") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tempat">
                        <ItemTemplate>
                            <asp:Label ID="Tempat" runat="server" Text='<%# Bind("Tempat") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pengajar">
                        <ItemTemplate>
                            <asp:Label ID="NamaPengajar" runat="server" Text='<%# Bind("NamaPengajar") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pembantu Pengajar">
                        <ItemTemplate>
                            <asp:Label ID="NamaPembantuPengajar" runat="server" Text='<%# Bind("NamaPembantuPengajar") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pembantu Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="NamaPembantuPelajar" runat="server" Text='<%# Bind("NamaPembantuPelajar")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="#CCCCCC" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export  " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>

</table>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_pelajar_list_mark_full.ascx.vb" Inherits="permatapintar.koko_pelajar_list_mark_full" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<asp:Label ID="lblMsgTop" runat="server" Text="A:Kehadiran. B:Jawatan. C:Penglibatan. D:Pencapaian."></asp:Label>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar|<asp:Label ID="lblKOKOName" runat="server" Text=''></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="StudentID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NAMA PELAJAR">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KELAS">
                        <ItemTemplate>
                            <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD#">
                        <ItemTemplate>
                            <asp:Label ID="lblMYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#PELAJAR">
                        <ItemTemplate>
                            <asp:Label ID="NoPelajar" runat="server" Text='<%# Bind("NoPelajar")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BANGSA">
                        <ItemTemplate>
                            <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JANTINA">
                        <ItemTemplate>
                            <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_KehadiranP1" runat="server" Text='<%# Bind("Uniform_KehadiranP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_JawatanP1" runat="server" Text='<%# Bind("Uniform_JawatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_PenglibatanP1" runat="server" Text='<%# Bind("Uniform_PenglibatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_PencapaianP1" runat="server" Text='<%# Bind("Uniform_PencapaianP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_JumlahP1" runat="server" Text='<%# Bind("Uniform_JumlahP1", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_KehadiranP1" runat="server" Text='<%# Bind("Persatuan_KehadiranP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_JawatanP1" runat="server" Text='<%# Bind("Persatuan_JawatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_PenglibatanP1" runat="server" Text='<%# Bind("Persatuan_PenglibatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_PencapaianP1" runat="server" Text='<%# Bind("Persatuan_PencapaianP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_JumlahP1" runat="server" Text='<%# Bind("Persatuan_JumlahP1", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_KehadiranP1" runat="server" Text='<%# Bind("Sukan_KehadiranP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_JawatanP1" runat="server" Text='<%# Bind("Sukan_JawatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_PenglibatanP1" runat="server" Text='<%# Bind("Sukan_PenglibatanP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_PencapaianP1" runat="server" Text='<%# Bind("Sukan_PencapaianP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_JumlahP1" runat="server" Text='<%# Bind("Sukan_JumlahP1", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bonus">
                        <ItemTemplate>
                            <asp:Label ID="BonusP1" runat="server" Text='<%# Bind("BonusP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="JumlahP1" runat="server" Text='<%# Bind("JumlahP1", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MARKAH">
                        <ItemTemplate>
                            <asp:Label ID="MarkahP1" runat="server" Text='<%# Bind("MarkahP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GRED">
                        <ItemTemplate>
                            <asp:Label ID="GredP1" runat="server" Text='<%# Bind("GredP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PNG">
                        <ItemTemplate>
                            <asp:Label ID="PNGP1" runat="server" Text='<%# Bind("PNGP1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="15% KOKO">
                        <ItemTemplate>
                            <asp:Label ID="KOKOP1" runat="server" Text='<%# Bind("KOKOP1", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Paparan" />
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
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
            &nbsp;<asp:Button ID="btnGred" runat="server" Text="Kira Gred" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>

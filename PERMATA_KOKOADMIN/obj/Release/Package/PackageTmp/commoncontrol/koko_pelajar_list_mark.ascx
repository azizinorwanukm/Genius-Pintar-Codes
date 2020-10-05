<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_pelajar_list_mark.ascx.vb" Inherits="permatapintar.koko_pelajar_list_mark" %>

<script type="text/javascript">
    function PopupWindow(pageURL, title) {
        var targetWin = window.open(pageURL, title);
    }

</script>

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
        <td>Peperiksaan:</td>
        <td>
            <select name="selPeperiksaan" id="selPeperiksaan" style="width: 200px;" runat="server">
                <option value="P1">SEMESTER 1</option>
                <option value="P2">SEMESTER 2</option>
                <%-- <option value="P3">PEPERIKSAAN 3</option>
                <option value="P4">PEPERIKSAAN 4</option>--%>
            </select>
        </td>
    </tr>
    <tr>
        <td>Kelas:</td>
        <td>
            <asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />
            <asp:LinkButton ID="lnkMarkahPenuh" runat="server" Visible="false">Paparan Penuh</asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID="lblMsgTop" runat="server" Text="A:Kehadiran. B:Jawatan. C:Penglibatan. D:Pencapaian." ForeColor="Red"></asp:Label>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar|<asp:Label ID="lblJenis" runat="server" Text=''></asp:Label>|<asp:Label ID="lblKOKOName" runat="server" Text=''></asp:Label>
            &nbsp;<asp:Label ID="lblTahun" runat="server" Text=''></asp:Label>
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
                            <asp:Label ID="Uniform_Kehadiran" runat="server" Text='<%# Bind("Uniform_Kehadiran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_Jawatan" runat="server" Text='<%# Bind("Uniform_Jawatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_Penglibatan" runat="server" Text='<%# Bind("Uniform_Penglibatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_Pencapaian" runat="server" Text='<%# Bind("Uniform_Pencapaian")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Uniform_Jumlah" runat="server" Text='<%# Bind("Uniform_Jumlah", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_Kehadiran" runat="server" Text='<%# Bind("Persatuan_Kehadiran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_Jawatan" runat="server" Text='<%# Bind("Persatuan_Jawatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_Penglibatan" runat="server" Text='<%# Bind("Persatuan_Penglibatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_Pencapaian" runat="server" Text='<%# Bind("Persatuan_Pencapaian")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan_Jumlah" runat="server" Text='<%# Bind("Persatuan_Jumlah", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_Kehadiran" runat="server" Text='<%# Bind("Sukan_Kehadiran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="B">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_Jawatan" runat="server" Text='<%# Bind("Sukan_Jawatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_Penglibatan" runat="server" Text='<%# Bind("Sukan_Penglibatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_Pencapaian" runat="server" Text='<%# Bind("Sukan_Pencapaian")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Sukan_Jumlah" runat="server" Text='<%# Bind("Sukan_Jumlah", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bonus">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBonus" runat="server" Width="50px" MaxLength="3" Text='<%# Bind("Bonus", "{0:n0}")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jum.">
                        <ItemTemplate>
                            <asp:Label ID="Jumlah" runat="server" Text='<%# Bind("Jumlah", "{0:N2}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MARKAH">
                        <ItemTemplate>
                            <asp:Label ID="Markah" runat="server" Text='<%# Bind("Markah")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GRED">
                        <ItemTemplate>
                            <asp:Label ID="Gred" runat="server" Text='<%# Bind("Gred")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PNG">
                        <ItemTemplate>
                            <asp:Label ID="PNG" runat="server" Text='<%# Bind("PNG")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="15% KOKO">
                        <ItemTemplate>
                            <asp:Label ID="KOKO" runat="server" Text='<%# Bind("KOKO", "{0:N2}")%>'></asp:Label>
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
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />
            &nbsp;<asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
            &nbsp;<asp:Button ID="btnGred" runat="server" Text="Kira Gred" CssClass="fbbutton" Visible="true" />
            &nbsp;<asp:Button ID="btnGenSijil" runat="server" Text="Jana Sijil" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>
<asp:Label ID="lblFieldname" runat="server" Text=''></asp:Label>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kemaskini_maklumat_pengumuman.ascx.vb" Inherits="permatapintar.kemaskini_maklumat_pengumuman" %>

<table class="fbform" style="height:150px;margin-bottom:10px">
    <tr class="fbform_header">
        <td colspan="2">Senarai Tanggungjawab Instruktor Kelab Dan Persatuan</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ID" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pengumuman" ItemStyle-Width="700">
                        <ItemTemplate>
                            <asp:Label ID="lblPengumuman" runat="server" Text='<%# Eval("Pengumuman") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPengumuman" width="700px" runat="server" Text='<%# Eval("Pengumuman") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jenis"  ItemStyle-Width="165">
                        <ItemTemplate>
                            <asp:Label ID="lblJenis" runat="server" Text='<%# Eval("Jenis_Kokurikulum") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowEditButton="True" HeaderText="[Pilih]" />
                    <asp:CommandField SelectText="[Buang]" ShowDeleteButton="True" HeaderText="[Buang]" />

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
            <asp:Button ID="btnCreate" runat="server" Text="Tambah Pengumuman" CssClass="fbbutton" Visible="true" />&nbsp;
        </td>
    </tr>
</table>

<table class="fbform" style="height:150px;margin-bottom:20px">
    <tr class="fbform_header">
        <td colspan="2">Senarai Tanggungjawab Instruktor Badan Beruniform</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="KokoBadan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ID" OnRowCancelingEdit="OnRowCancelingEditBadan" OnRowUpdating="OnRowUpdatingBadan"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pengumuman" ItemStyle-Width="700">
                        <ItemTemplate>
                            <asp:Label ID="lblPengumuman" runat="server" Text='<%# Eval("Pengumuman") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPengumuman" width="700px" runat="server" Text='<%# Eval("Pengumuman") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jenis" ItemStyle-Width="165">
                        <ItemTemplate>
                            <asp:Label ID="lblJenis" runat="server" Text='<%# Eval("Jenis_Kokurikulum") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowEditButton="True" HeaderText="[Pilih]" />
                    <asp:CommandField SelectText="[Buang]" ShowDeleteButton="True" HeaderText="[Buang]" />

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
            <asp:Button ID="BtnCreateUniform" runat="server" Text="Tambah Pengumuman" CssClass="fbbutton" Visible="true" />&nbsp;
        </td>
    </tr>
</table>

<table class="fbform" style="height:150px;margin-bottom:20px">
    <tr class="fbform_header">
        <td colspan="2">Senarai Tanggungjawab Instruktor Sukan Dan Permainan</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="KokoSukan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ID" OnRowCancelingEdit="OnRowCancelingEditSukan" OnRowUpdating="OnRowUpdatingSukan"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pengumuman" ItemStyle-Width="700">
                        <ItemTemplate>
                            <asp:Label ID="lblPengumuman" runat="server" Text='<%# Eval("Pengumuman") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPengumuman" width="700px" runat="server" Text='<%# Eval("Pengumuman") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jenis" ItemStyle-Width="165">
                        <ItemTemplate>
                            <asp:Label ID="lblJenis" runat="server" Text='<%# Eval("Jenis_Kokurikulum") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowEditButton="True" HeaderText="[Pilih]" />
                    <asp:CommandField SelectText="[Buang]" ShowDeleteButton="True" HeaderText="[Buang]" />

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
            <asp:Button ID="BtnCreateSukan" runat="server" Text="Tambah Pengumuman" CssClass="fbbutton" Visible="true" />&nbsp;
        </td>
    </tr>
</table>

<table class="fbform" style="height:150px;margin-bottom:20px"> 
    <tr class="fbform_header">
        <td colspan="2">Senarai Dokumen</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="DokumenView" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ContentID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dokumen" ItemStyle-Width="865">
                        <ItemTemplate>
                            <asp:Label ID="DokumenName" runat="server" Text='<%# Eval("DokumenName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Buang]" ShowDeleteButton="True" HeaderText="[Buang]" />

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
            <asp:Button ID="btnAddDoc" runat="server" Text="Tambah Dokumen" CssClass="fbbutton" Visible="true" />&nbsp;
        </td>
    </tr>
</table>
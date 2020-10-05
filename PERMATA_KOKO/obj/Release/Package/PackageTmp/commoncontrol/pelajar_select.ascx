<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_select.ascx.vb" Inherits="permatapintar.pelajar_select" %>

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
        <td>Status:</td>
        <td>
            <select name="selStatusTawaran" id="selStatusTawaran" style="width: 200px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="TERIMA" selected="selected">TERIMA</option>
                <option value="TOLAK">TOLAK</option>
            </select></td>
    </tr>
    <tr>
        <td>Jantina:
        </td>
        <td>
            <select name="selStudentGender" id="selStudentGender" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="LELAKI">LELAKI</option>
                <option value="PEREMPUAN">PEREMPUAN</option>
            </select>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
        <td>#Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtNoPelajar" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar
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
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD#">
                        <ItemTemplate>
                            <asp:Label ID="lblMYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="NoPelajar" runat="server" Text='<%# Bind("NoPelajar")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bangsa">
                        <ItemTemplate>
                            <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Badan Beruniform">
                        <ItemTemplate>
                            <asp:Label ID="Uniform" runat="server" Text='<%# Bind("Uniform")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kelab&Persatuan">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan" runat="server" Text='<%# Bind("Persatuan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sukan&Permainan">
                        <ItemTemplate>
                            <asp:Label ID="Sukan" runat="server" Text='<%# Bind("Sukan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rumah Sukan">
                        <ItemTemplate>
                            <asp:Label ID="Rumahsukan" runat="server" Text='<%# Bind("Rumahsukan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kelas">
                        <ItemTemplate>
                            <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pilih">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
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
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnAssign" runat="server" Text="Tetapkan Kelas" CssClass="fbbutton" Visible="true" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>

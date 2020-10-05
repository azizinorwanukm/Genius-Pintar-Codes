<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelaskoko_view.ascx.vb" Inherits="permatapintar.kelaskoko_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Kumpulan
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sukan & Permainan:
        </td>
        <td>
            <asp:Label ID="lblKOKOName" runat="server" Text=''></asp:Label>&nbsp;<asp:Label ID="lblTahun" runat="server" Text=''></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Kumpulan:
        </td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Instruktor:
        </td>
        <td>
            <asp:Label ID="lblInstruktor" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" id="lbldatrespondant" runat="server">
            <table class="fbform">
                <tr class="fbform_header">
                    <td>Senarai Pelajar</td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 170px; overflow: auto">
                            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="StudentID"
                                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                                <Columns>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nama">
                                        <ItemTemplate>
                                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MYKAD">
                                        <ItemTemplate>
                                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kelas">
                                        <ItemTemplate>
                                            <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                                    HorizontalAlign="Left" />
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
            &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kumpulan</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblKokoID" runat="server" Text="" ForeColor="red"></asp:Label>

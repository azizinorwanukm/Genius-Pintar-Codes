<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_pencapaian_update.ascx.vb" Inherits="permatapintar.koko_pencapaian_update" %>

<script type="text/javascript">
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    }

</script>

<asp:Label ID="lblMsgTop" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Kemaskini Penyertaan dan Pencapaian Tahun&nbsp;<asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;<asp:LinkButton ID="lnkSample" runat="server" OnClientClick="PopupCenter('pelajar.koko.pencapaian.sample.aspx','Contoh',700,450)">Contoh</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>Penyertaan dan Pencapaian:
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <asp:TextBox ID="txtPencapaian" runat="server" Width="98%" TextMode="MultiLine" Rows="15"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>Disahkan?:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDisahkan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Disahkan Oleh:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDisahkanOleh" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <table class="fbform" id="Test">
                <tr class="fbform_header">
                    <td colspan="2">Senarai Kokurikulum
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="Tahun"
                            Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tahun">
                                    <ItemTemplate>
                                        <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pencapaian">
                                    <ItemTemplate>
                                        <asp:Label ID="Pencapaian" runat="server" Text='<%# Bind("Pencapaian")%>'></asp:Label>
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
                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>


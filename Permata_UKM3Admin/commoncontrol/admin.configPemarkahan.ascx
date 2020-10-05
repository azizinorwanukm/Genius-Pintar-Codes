<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.configPemarkahan.ascx.vb" Inherits="permatapintar.admin_configPemarkahan" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>
<asp:label runat="server" ForeColor ="Red">catatan: 1 adalah format permarkahan yang digunakan</asp:label>
<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    
    <tr>
        <td>
            <asp:GridView ID="datRespondent" DataKeyNames="id_formula" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Formula Permarkahan">
                    <ItemTemplate>
                    <asp:Label ID="lbl_formula" runat="server" Text='<%# Bind("formula") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tahun">
                    <ItemTemplate>
                    <asp:Label ID="lbl_tahun" runat="server" Text='<%# Bind("year") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="PPCS Date">
                    <ItemTemplate>
                    <asp:Label ID="lbl_ppcsdate" runat="server" Text='<%# Bind("session") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                    <ItemTemplate>
                    <asp:Label ID="catatan" runat="server" Text='<%# Bind("isActive") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>                    
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" HeaderText="Edit"/>
                    
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>

    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            
    <asp:Button runat="server" ID="btnDelete" Text="Buang"/>&nbsp;
    <asp:Button runat="server" ID="btnSetActive" Text="Pilih" />&nbsp;
            <asp:Button runat="server" ID="btnFormulaAdd" Text="Tambah formula" />
        </td>
        </tr><tr>
         <td>
            <asp:label runat="server" ForeColor ="Red" >Untuk Memilih formula permarkahan,tandakan checkbox dan pastikan PPCS Date sama dengan sesi terkini dalam sistem </asp:label>
            <br />
            <br />
            <asp:label runat="server" ForeColor ="Red"> Untuk membuang formula permarkahan,tandakan checkbox dahulu.</asp:label>
        </td>
    </tr>
</table>


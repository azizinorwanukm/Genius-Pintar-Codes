<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.kelayakanKolej.config.ascx.vb" Inherits="permatapintar.admin_kelayakanKolej_config1" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

    <table class="fbform">
        <tr>
            <td>Tarikh Mula Kemasukan Data Pelajar Ke Kolej : </td>
            <td><asp:TextBox ID="txtkolejDateStart" runat="server" Width="200px" MaxLength="200" placeholder="e.g. 20181209"></asp:TextBox></td>
            <%--<td><asp:RadioButtonList ID="radioStem" runat="server" EnableViewState="False" RepeatDirection="horizontal" RepeatLayout="Flow" >
                <asp:ListItem Value="1" Text="Aktif"></asp:ListItem>
                <asp:ListItem Value="0" Text="Tidak Aktif"></asp:ListItem>
            </asp:RadioButtonList></td>--%>
        </tr>
        <tr>
            <td>Tarikh Tamat Kemasukan Data Pelajar Ke Kolej : </td>
            <td><asp:TextBox ID="txtkolejDateEnd" runat="server" Width="200px" MaxLength="200" placeholder="e.g. 20181209"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td>&nbsp</td>
        </tr>
        <tr>
            <td>Sesi : </td>
            <td><asp:DropDownList ID="ddlSesiKolej" runat="server" Width="200px" MaxLength="200"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2"><asp:button ID="btnEnable" runat="server" text="Kemaskini" CssClass="fbbutton" class="btn btn-primary active" /></td>
        </tr>
    </table>



    &nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Session
        </td>
    </tr>
    <tr>
        <td>
            <div id="exam_info" runat ="server">
                   <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
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
                    <asp:TemplateField HeaderText="Nama Sesi">
                    <ItemTemplate>
                    <asp:Label ID="lbl_sessionName" runat="server" Text='<%# Bind("sessionName") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tahun">
                    <ItemTemplate>
                    <asp:Label ID="lbl_tahun" runat="server" Text='<%# Bind("ukm3Year") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian">
                    <ItemTemplate>
                    <asp:Label ID="lbl_examName" runat="server" Text='<%# Bind("exam_name") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCS Date">
                    <ItemTemplate>
                    <asp:Label ID="lbl_ppcsdate" runat="server" Text='<%# Bind("ppcsdate") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                    <ItemTemplate>
                    <asp:Label ID="catatan" runat="server" Text='<%# Bind("catatan") %>' > </asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" HeaderText="Edit"/>
                              
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>

            </div>

            
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:button ID="btnAdd" runat="server" text="Tambah Session" CssClass="fbbutton" class="btn btn-primary active" />&nbsp
        
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fbbutton" class="btn btn-primary active" />&nbsp;
        </td>
    </tr>
</table>
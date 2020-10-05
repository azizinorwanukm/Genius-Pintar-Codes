<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.petugas_Dilantik.ascx.vb" Inherits="permatapintar.admin_petugas_Dilantik" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Petugas
        </td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="200px" MaxLength="150" placeholder="Nama,IC,Id Staf" ></asp:TextBox>&nbsp;
        </td>
    </tr>  
    <tr>
        <td class="fbtd_left">Jenis Peperiksaan:
        </td>
        <td>:</td>
        <td>
            <asp:dropdownlist ID="ddl_Sesi" runat="server" Width="200px" MaxLength="150" placeholder="Jenis Peperiksaan"></asp:dropdownlist>&nbsp;
        </td>
    </tr>
     <tr>
        <td class="fbtd_left">Jawatan
        </td>
        <td>:</td>
        <td>
            <asp:dropdownlist ID="ddlJawatan" runat="server" Width="200px" MaxLength="150" placeholder="Jawatan">
               
            </asp:dropdownlist>&nbsp;
        </td>
    </tr>  
    <tr>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
</table>
&nbsp;
<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Petugas UKM3 Yang Dilantik
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" DataKeyNames="staff_id,staff_name,staff_login" runat="server" AutoGenerateColumns="False" AllowPaging="True"
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
                    <asp:TemplateField HeaderText="Nama Petugas">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("staff_name") %>'></asp:Label>
                            
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Peperiksaan">
                        <ItemTemplate>
                            <asp:Label ID="Pwd" runat="server" Text='<%# Bind("staff_session") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Jawatan">
                        <ItemTemplate>
                            <asp:Label ID="jawatan" runat="server" Text='<%# Bind("staff_Position") %>'></asp:Label>
                        </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>                    
                     <asp:TemplateField HeaderText="Login ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_loginID" runat="server" Text='<%# Bind("staff_login") %>'></asp:Label>
                        </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>         
                    
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar Kata Laluan" />
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
            <asp:Button ID="btnRemove" runat="server" Text="Buang" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>

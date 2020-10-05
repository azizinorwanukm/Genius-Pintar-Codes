<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_petugas_list.ascx.vb"
    Inherits="permatapintar.PusatUjian_Petugas_list" %>

<script type="text/javascript">
     function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtTarikhSearch.ClientID %>'), 'PERMATApintar', 'yyyy-MM-dd');
     }

    function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
    }

</script>

<table class="fbform">
    <tr class="fbform_header">
        <td>Carian.
        </td>
    </tr>
    <tr>
        <td>Tarikh:
            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtTarikhSearch" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>
            &nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    
    <tr>
        <td>
            <i>Nota:Kosongkan Tarikh untuk carian semua.</i>
        </td>
    </tr>
</table>
<br />
<table class="fbform" width="100%">
    <tr class="fbform_header">
        <td>Senarai Petugas Pusat Ujian
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PusatIDPetugasID"
                Width="100%" PageSize="25">
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
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="Tarikh" runat="server" Text='<%# Bind("Tarikh") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAGI">
                        <ItemTemplate>
                            <asp:Label ID="SesiPagi" runat="server" Text='<%# Bind("SesiPagi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TGHARI">
                        <ItemTemplate>
                            <asp:Label ID="SesiTghari" runat="server" Text='<%# Bind("SesiTghari") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PETANG">
                        <ItemTemplate>
                            <asp:Label ID="SesiPetang" runat="server" Text='<%# Bind("SesiPetang") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. Tel#">
                        <ItemTemplate>
                            <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Petugas">
                        <ItemTemplate>
                            <asp:Label ID="UserType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
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
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>

<table class="fbform">
    <tr>
        <td>
            <asp:Button ID="btnUnAssign" runat="server" Text="Un-Assign" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;Sessi:<asp:CheckBox
                ID="chkPagi" runat="server" Text="PAGI" />&nbsp;<asp:CheckBox ID="chkTghari" runat="server"
                    Text="TENGAHARI" />&nbsp;<asp:CheckBox ID="chkPetang" runat="server" Text="PETANG" />
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr><td><i>Nota: Pilih tarikh di atas untuk kemaskini Tarikh dan Sessi.</i></td></tr>
</table>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>

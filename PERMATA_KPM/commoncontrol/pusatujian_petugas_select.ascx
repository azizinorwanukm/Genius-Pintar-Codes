<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_petugas_select.ascx.vb"
    Inherits="permatapintar.pusatujian_petugas_select" %>

<script type="text/javascript">
       function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }


    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtTarikhSearch.ClientID %>'), 'PERMATApintar', 'yyyy-MM-dd');
    }

</script>

<table class="fbform" width="100%">
    <tr class="fbform_header">
        <td>Senarai Petugas
        </td>
    </tr>
    <tr>
        <td>Jenis Petugas:<select name="selUserType" id="selUserType" style="width: 200px;" runat="server">
            <option value="ALL">ALL</option>
            <option value="PEMANTAU JPN">PEMANTAU JPN</option>
            <option value="PEMANTAU KPM">PEMANTAU KPM</option>
            <option value="PEMANTAU PPD">PEMANTAU PPD</option>
            <option value="PEMANTAU UKM">PEMANTAU UKM</option>
            <option value="PENGAWAS">PENGAWAS</option>
            <option value="JURUTEKNIK">JURUTEKNIK</option>
        </select>&nbsp;Nama Petugas:<asp:TextBox ID="txtFullname" runat="server" Width="200px"
            MaxLength="250"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Pilih Petugas Pusat Ujian
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PetugasID"
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
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="City" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="State" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Petugas">
                        <ItemTemplate>
                            <asp:Label ID="UserType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="Pilih" />
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
    <tr>
        <td style="vertical-align: middle;">
            <asp:Button ID="btnAssign" runat="server" Text="Assign to Pusat" CssClass="fbbutton" />&nbsp;
            &nbsp;Tarikh:
           

            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtTarikhSearch" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>

            Sessi:<asp:CheckBox ID="chkPagi" runat="server" Text="PAGI" />&nbsp;<asp:CheckBox
                ID="chkTghari" runat="server" Text="TENGAHARI" />&nbsp;<asp:CheckBox ID="chkPetang"
                    runat="server" Text="PETANG" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkSenaraiPetugas"
                        runat="server">Senarai Petugas</asp:LinkButton>
            <a href="#Bottom"></a>
        </td>
    </tr>
</table>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
